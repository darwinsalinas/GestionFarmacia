using GestionFarmacia.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionFarmacia.Startup))]
namespace GestionFarmacia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context2 = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context2));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context2));



            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Administrador"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";

                string userPWD = "12345678";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Administrador");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Regente"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Regente";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "regente1";
                user.Email = "regente1@gmail.com";
                string userPWD = "12345678";

                var regente1 = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (regente1.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Regente");

                }


                var user2 = new ApplicationUser();
                user2.UserName = "regente2";
                user2.Email = "regente2@gmail.com";

                var regente2 = UserManager.Create(user2, userPWD);

                //Add default User to Role Admin   
                if (regente2.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user2.Id, "Regente");

                }


            }
        }
    }
}
