using MyBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudget.BusinessLogic
{
    /// <summary>
    /// Business Logic for Categories
    /// </summary>
    public class CategoryService
    {
        private ApplicationDbContext _context;
        private Category _category;
        private int _categoryId;
        private string _userId;

        public CategoryService(int categoryId, string userId)
        {
            _context = new ApplicationDbContext();
            _category = _context.Categories.SingleOrDefault(c => c.Id == categoryId);
            if (_category == null)
                throw new NullReferenceException();

            _categoryId = _category.Id;
            _userId = userId;
        }

        /// <summary>
        /// Set "Без категории" for connected transactions, delete category
        /// </summary>
        public void DeleteCategory()
        {
            if (_category.IsSystem)
                throw new Exception("System Category can not be deleted");

            var editUser = _context.Users.Find(_userId);

            using (var SqlTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Update Transactions
                    int DefCatId = _category.IsSpendingCategory ? Category.SpendingCategoryDefault : Category.IncomeCategoryDefault;
                    var transactions = _context.Transactions.Where(c => c.CategoryId == _categoryId).ToList();
                    transactions.ForEach(t => t.CategoryId = DefCatId);

                    // Delete from MyCategories
                    editUser.Categories.Remove(_category);

                    // Delete from Categories if it's not common
                    if (!String.IsNullOrEmpty(_category.CreatedBy))
                    {
                        _context.Categories.Remove(_category);
                    }

                    _context.SaveChanges();
                    SqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    SqlTransaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}