using FluentWebApplication1.Classes;
using Microsoft.AspNetCore.Mvc.RazorPages;
#nullable disable

namespace FluentWebApplication1.Pages
{
    /// <summary>
    /// Represents the model for the "About" page in the application.
    /// </summary>
    /// <remarks>
    /// This class is used to handle the logic and data for the "About" Razor Page.
    /// It inherits from <see cref="PageModel"/> and provides functionality to retrieve
    /// and display the current page name.
    /// </remarks>
    public class AboutModel : PageModel
    {
        /// <summary>
        /// Gets the name of the current page.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> representing the name of the current page. 
        /// This value is determined by the HTTP request and is set during the <c>OnGet</c> method execution.
        /// </value>
        /// <remarks>
        /// The property is populated using the <see cref="PageHelpers.GetCurrentPageName(HttpRequest)"/> method, 
        /// which extracts the page name from the current HTTP request.
        /// </remarks>
        public string CurrentPageName { get; private set; }
        /// <summary>
        /// Handles the HTTP GET request for the "About" page.
        /// </summary>
        public void OnGet()
        {
 
            CurrentPageName = PageHelpers.GetCurrentPageName(HttpContext.Request);
        }
    }
}
