using System;
using System.Web;
using System.Web.Security;
using Model;

namespace Sys.Inventarios.Helpers
{
    /// <summary>
    /// Checa el siguiente link para que puedas leer una explicación más detallada
    /// </summary>
    // https://anexsoft.com/clase-para-agilizar-la-autenticacion-en-asp-net-mvc

    public class SessionHelper
    {
        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }
        public static int GetUser()
        {
            int user_id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        }

        public static void AddUserToSession(string id, bool persist)
        {
            var cookie = FormsAuthentication.GetAuthCookie("UserInventory", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            //No es recomendable tener una cookie persistente, es un riesgo de seguridad
            // https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#expire-and-max-age-attributes
            // Lo ideal es que tuvieran el secure attribute pero necesitariamos tener un dominio 
            // HTTPS en vez de HTTP
            cookie.Expires = DateTime.Now.AddMonths(1);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void ActualizarSession(Usuario Usuario)
        {
            // Las apps web son stateless, o sea, el estado (la información que estás operando) se borra.
            // Cada que se genera un request-response, se limpia toda la información de tu app. Con las variables
            // de sesión, guardas información en el servidor, de tal manera que la puedes recuperar aunque hagas varias request-responses.
            // Como su nombre lo indica, se guardan mientras dure la sesión. 
            HttpContext.Current.Session["Usuario_ID"] = Usuario.Id;
            HttpContext.Current.Session["CorreoElectronico"] = Usuario.CorreoElectronico;
            HttpContext.Current.Session["EmpresaId"] = Usuario.EmpresaId;
            HttpContext.Current.Session["Rol_Id"] = Usuario.Rol_Id;
            HttpContext.Current.Session["Nombres"] = Usuario.Nombres;
        }

    }
}