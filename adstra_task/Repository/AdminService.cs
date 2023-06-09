﻿using adstra_task.Models;
using adstra_task.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace adstra_task.Repository
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminService(UserManager<ApplicationUser> userManager
            ,RoleManager<IdentityRole> roleManager
            ,IMapper mapper)
        {
            this.userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }



        public async Task<IdentityResult> CreateRole(CreateRoleViewModel model)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(model.RoleName.ToString().ToLower()));
            
            return result;

        }

        public IEnumerable<UsersViewModel> AllUsers()
        {
            var uers = userManager.Users;
            return _mapper.Map<IEnumerable<UsersViewModel>>(uers); 
        }
        public IEnumerable<RolesViewModel> AllRoles()
        {
            var roles = _roleManager.Roles;

            return _mapper.Map<IEnumerable<RolesViewModel>>(roles);
        }
        public async  Task<List<UserRoleManager>> ManageRolesGet(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
           
            var model = new List<UserRoleManager>();


            foreach (var role in _roleManager.Roles)
            {
                var userRoleModel = new UserRoleManager
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };
                var roleName = await userManager.IsInRoleAsync(user, role.Name);
                if (roleName)
                {
                    userRoleModel.IsSelected = true;
                }
                else
                {
                    userRoleModel.IsSelected = false;
                }
                model.Add(userRoleModel);
            };
            

            return model;
        }

        public async Task<IdentityResult> ManageRolesPost(List<UserRoleManager> model,string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);


            result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(x => x.RoleName));

            return result;
        }

        public async Task<IdentityResult> DeleteUserPost(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            
            var roleResult = await userManager.IsInRoleAsync(user, "admin");
            if (roleResult == false)
            {
                var result = await userManager.DeleteAsync(user);
            }

            return IdentityResult.Failed();
        }


    }
}
