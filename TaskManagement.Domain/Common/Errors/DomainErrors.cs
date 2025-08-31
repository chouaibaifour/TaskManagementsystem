using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Common.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly DomainError EmailRequired = 
                                new DomainError("User.Email.Required", "Email is required.");

            public static readonly DomainError EmailInvlid =
                                new DomainError("User.Email.Invalid", "Email is invalid.");

            public static readonly DomainError EmailDuplicated =
                                new DomainError("User.Email.Duplicated", "Email is already registered.");

            public static readonly DomainError FullNameRequired =
                                new DomainError("User.FullName.Required", "Full name is required.");

            public static readonly DomainError PasswordTooWeak =
                                new DomainError("User.Password.Weak", "Password is too weak.");

            public static readonly DomainError PasswordRequired =
                                new DomainError("User.Password.Required", "Password is required.");

            public static readonly DomainError NotFound =
                                new DomainError("User.NotFound", "User not found.");

            public static readonly DomainError UserAlreadyExists =
                                new DomainError("User.AlreadyExists", "User already exists.");

            public static readonly DomainError RoleIvalidTransition =
                                new DomainError("User.Role.InvalidTransition", "Role transition is invalid.");

            public static readonly DomainError UserNotActive =
                                new DomainError("User.NotActive", "User is not active.");
            public static readonly DomainError UserNotAuthorized =
                                new DomainError("User.NotAuthorized", "User is not authorized to perform this action.");

            public static readonly DomainError UserNotAuthenticated =
                                new DomainError("User.NotAuthenticated", "User is not authenticated.");

            public static readonly DomainError UserWrongCredential =
                                new DomainError("User.WrongCredential", "Email or Password is incorrect.");

        }

        public static class Project
        {
            public static readonly DomainError NameRequired =
                                new DomainError("Project.Name.Required", 
                                                "Project name is required.");

            public static readonly DomainError NotFound =
                                new DomainError("Project.NotFound", 
                                                "Project not found.");

            public static readonly DomainError UserNotAuthorized =
                                new DomainError("Project.User.NotAuthorized", 
                                                "User is not authorized to access this project.");

            public static readonly DomainError ProjectAlreadyExistsWithThisName =
                                new DomainError("Project.AlreadyExists",
                                                "Project with the same name already exists.");

            public static readonly DomainError DescriptionTooLong =
                                new DomainError("Project.Description.TooLong",
                                                "Project description is too long.");
            public static readonly DomainError DescriptionRequired =
                                new DomainError("Project.Description.Required",
                                                "Project description is Required.");

            public static readonly DomainError InvalidStatusTransition =
                                new DomainError("Project.ProjectStatus.InvalidTransition",
                                                "Project status transition is invalid.");
            public static readonly DomainError ProjectStatusRequired =
                                new DomainError("Project.ProjectStatus.Required",
                                                "Project status is required.");

        }

    }
}
