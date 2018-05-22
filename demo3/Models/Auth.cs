using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace demo3.Controllers
{
    public class Auth
    {
        private string userid { get; set; }
        private string first_name { get; set; }
        private string last_name { get; set; }
        private string email { get; set; }
        private string roles { get; set; }

        public string getUserID() { return userid; }
        public string getFirstName() { return first_name; }
        public string getLastName() { return last_name; }
        public string getEmail() { return email; }
        public string getRoles() { return roles; }

        public Auth()
        {           
            var claims = System.Security.Claims.ClaimsPrincipal.Current.Claims;
            //foreach (var claim in claims)
            //    Utility.WriteLog("Auth", claim.Type.ToString() +" | "+ claim.Value.ToString());

            userid = claims.Where(c => c.Type == "name").Select(c => c.Value).SingleOrDefault();
            first_name = claims.Where(c => c.Type == "given_name").Select(c => c.Value).SingleOrDefault();
            last_name = claims.Where(c => c.Type == "family_name").Select(c => c.Value).SingleOrDefault();
            email = claims.Where(c => c.Type == "email").Select(c => c.Value).SingleOrDefault();
            if (claims.Where(c => c.Type == "role").Select(c => c.Value).Count() != 0 )
            {
                roles = (claims.Where(c => c.Type == "role").Select(c => c.Value).ToList()).Aggregate((a, b) => a + "," + b);
            }
        }

        public bool checkRolePermission(string role)
        {
            foreach (string item in role.Split('|'))
            {
                if (roles.IndexOf(item) != -1)
                    return true;
            }
            return false;
        }
    }
}