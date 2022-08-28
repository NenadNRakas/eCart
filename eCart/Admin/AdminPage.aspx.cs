using eCart.Logic;
using eCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCart.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string productAction = Request.QueryString["ProductAction"];
            if (productAction == "add")
            {
                LabelAddStatus.Text = "Product added!";
            }
            if (productAction == "remove")
            {
                LabelRemoveStatus.Text = "Product removed!";
            }
        }
        protected void AddProductButton_Click(object sender, EventArgs e)
        {
            /*Boolean fileOK = false;
            String path = Server.MapPath("~/Images/Thumbs/");
            if (ProductImage.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(ProductImage.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }
            if (fileOK)
            {
                try
                {
                    // Save to Images folder.
                    ProductImage.PostedFile.SaveAs(path + ProductImage.FileName);
                    // Save to Images/Thumbs folder.
                    //ProductImage.PostedFile.SaveAs(path + "Thumbs/" + ProductImage.FileName);
                }
                catch (Exception ex)
                {
                    LabelAddStatus.Text = ex.Message;
                }*/
            /*try
            {
                file.ReadBlock(buffer, index, buffer.Length);
            }
            catch (FileNotFoundException e)
            {
                Server.Transfer("NoFileErrorPage.aspx", true);
            }
            catch (System.IO.IOException e)
            {
                Server.Transfer("IOErrorPage.aspx", true);
            }
            finally
            {
                if (file != null) { file.Close(); }
            }*/
            Product product = new Product();
            string categoryImage = "AiCloseup.png";
            if (DropDownAddCategory.SelectedIndex < 0) { categoryImage = "AiCloseup.png"; }
            /*if (DropDownAddCategory.SelectedIndex == 0) { categoryImage = "Access.png"; }
            if (DropDownAddCategory.SelectedIndex == 1) { categoryImage = "Privacy.png"; }
            if (DropDownAddCategory.SelectedIndex == 2) { categoryImage = "Index.png"; }
            if (DropDownAddCategory.SelectedIndex == 3) { categoryImage = "Security.png"; }
            if (DropDownAddCategory.SelectedIndex == 4) { categoryImage = "Telecoms.png"; }*/
            switch (DropDownAddCategory.SelectedIndex)
            {
                case 0:
                    categoryImage = "Access.png";
                    break;
                case 1:
                    categoryImage = "Privacy.png";
                    break;
                case 2:
                    categoryImage = "Index.png";
                    break;
                case 3:
                    categoryImage = "Security.png";
                    break;
                case 4:
                    categoryImage = "Telecoms.png";
                    break;
                default:
                    categoryImage = "AiDistance.png";
                    break;
            }
            if (DropDownAddCategory.SelectedIndex > 4) { categoryImage = "AiDistance.png"; }
            // Add product data to DB.
            AddProducts products = new AddProducts();
                bool addSuccess = products.AddProduct(AddProductName.Text, AddProductDescription.Text,
                    AddProductPrice.Text, DropDownAddCategory.SelectedValue, ProductImage.FileName);
                if (addSuccess)
                {
                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProductAction=add");
                }
                else
                {
                    LabelAddStatus.Text = "Unable to add new product to database.";
                }
            /*}
            else
            {
                LabelAddStatus.Text = "Unable to accept file type.";
            }*/
        }
        public IQueryable GetCategories()
        {
            var _db = new eCart.Models.ProductContext();
            IQueryable query = _db.Categories;
            return query;
        }
        public IQueryable GetProducts()
        {
            var _db = new eCart.Models.ProductContext();
            IQueryable query = _db.Products;
            return query;
        }
        protected void RemoveProductButton_Click(object sender, EventArgs e)
        {
            using (var _db = new eCart.Models.ProductContext())
            {
                int productId = Convert.ToInt16(DropDownRemoveProduct.SelectedValue);
                var myItem = (from c in _db.Products where c.ProductID == productId select c).FirstOrDefault();
                if (myItem != null)
                {
                    _db.Products.Remove(myItem);
                    _db.SaveChanges();

                    // Reload the page.
                    string pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
                    Response.Redirect(pageUrl + "?ProductAction=remove");
                }
                else
                {
                    LabelRemoveStatus.Text = "Unable to locate product.";
                }
            }
        }
    }
}