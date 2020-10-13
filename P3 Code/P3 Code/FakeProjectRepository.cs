﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P3_Code
{
    class FakeProjectRepository : IProjectRepository
    {
        public const string NO_ERROR = "";
        public const string MODIFIED_PROJECT_ID_ERROR = "Can not modify the project id";
        public const string DUPLICATE_PROJECT_NAME_ERROR = "Project name already exists";
        public const string NO_PROJECT_FOUND_ERROR = "";
        public const string EMPTY_PROJECT_NAME_ERROR = "Project name is empty or blank";
        private static List<Project> projects;
        private static int nextId = 0;


        public FakeProjectRepository()
        {
            if(projects == null)
            {
                projects = new List<Project>();

                projects.Add(new Project
                {
                    Id = 1,
                    Name = "Hello1"
                });
                projects.Add(new Project
                {
                    Id = 2,
                    Name = "Hello2"
                });
                projects.Add(new Project
                {
                    Id = 3,
                    Name = "Hello3"
                });
                projects.Add(new Project
                {
                    Id = 4,
                    Name = "Hello4"
                });
            }

        }

        public int GetNextID()
        {
            foreach (var Project in projects)
            {
                int currentId = Project.Id;
                if(currentId > nextId)
                {
                    nextId = currentId;
                }
            }
            nextId++;
            return (nextId);
        }

        public string Add(Project project, out int Id)
        {
            Id = GetNextID();

            //make sure user given name is valid
            project.Name = project.Name.Trim();
            project.Name.Trim(); //removes leading and trailing white spaces
            if (project.Name == null)
            {
                return (EMPTY_PROJECT_NAME_ERROR);
            }
            var isDuplicate = IsDuplicateName(project.Name);
            if(isDuplicate == true)
            {
                return (DUPLICATE_PROJECT_NAME_ERROR);
            }


            //add new project to list
            projects.Add(new Project
            {
                Id = Id,
                Name = project.Name
            });
            return (NO_ERROR);
        }

        public List<Project> GetAll()
        {
            return projects;
        }

        public bool IsDuplicateName(string projectName)
        {
            foreach (var Project in projects)
            {
                if (Project.Name == projectName)
                {
                    return (true);
                }
            }
            return (false);
        }

        public string Modify(int projectId, Project project)
        {
            int initialProjectId = projects[projectId].Id;

            project.Name = project.Name.Trim();
            project.Name.Trim(); //removes leading and trailing white spaces
            if (project.Name == null)
            {
                return (EMPTY_PROJECT_NAME_ERROR);
            }
            var isDuplicate = IsDuplicateName(project.Name);
            if (isDuplicate == true)
            {
                return (DUPLICATE_PROJECT_NAME_ERROR);
            }

            projects[projectId].Name = project.Name;

            //since the user has no way to modify the id, is this just a sanity check???? but it's required
            if(initialProjectId != projects[projectId].Id)
            {
                return (MODIFIED_PROJECT_ID_ERROR);
            }

            return (NO_ERROR);
        }

        public string Remove(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
