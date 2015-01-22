using System.Web.Mvc;
using System.Web.Security;

namespace SSO.Controllers
{
  public class AccountController : Controller
  {
    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      if (Request.IsAuthenticated)
      {
        return RedirectToAction("Index", "Home");
      }

      ViewBag.ReturnUrl = returnUrl;
      return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Login(string username, string password, string returnUrl)
    {
      if (FormsAuthentication.Authenticate(username, password))
      {
        FormsAuthentication.SetAuthCookie(username, false);
        if (!string.IsNullOrEmpty(returnUrl))
        {
          return Redirect(returnUrl);
        }
        else
        {
          return RedirectToAction("Index", "Home");
        }
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Invalid login details");
        ViewBag.ReturnUrl = returnUrl;
        return View();
      }
    }
  }
}