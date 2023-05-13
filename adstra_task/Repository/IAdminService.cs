﻿using adstra_task.Models;
using adstra_task.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace adstra_task.Repository
{
    public interface IAdminService
    {
        Task<IdentityResult> CreateRole(CreateRoleViewModel Name);
        IEnumerable<UsersViewModel> AllUsers();
        Task<List<UserRoleManager>> ManageRolesGet(string id);
        Task<IdentityResult> ManageRolesPost(List<UserRoleManager> model, string id);

    }
}