using ETT.Logic.DTO.Projects;
using ETT.Logic.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace ETT.Tests.Projects
{
    public class ProjectServiceTest
    {
        /*[Fact]
        public void CreateProject()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                Exception error = null;

                try
                {
                    int number = new Random().Next(1, int.MaxValue);

                    service.CreateProject(
                        new ProjectDTO()
                        {
                            Name = "Project " + number,
                            Description = "Description of the project " + number + "."
                        }
                    );
                }
                catch (Exception E)
                {
                    error = E;
                }

                // Assert
                Assert.Null(error);
            }
        }

        /// <summary>
        /// Set inserted project id
        /// </summary>
        private const int projectId = 1;


        [Fact]
        public void UpdateProject()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                Exception error = null;

                try
                {
                    int number = new Random().Next(1, int.MaxValue);

                    service.UpdateProject(
                        new ProjectDTO()
                        {
                            Id = projectId,
                            Name = "Project " + number + " updated",
                            Description = "Description of the updated project " + number + "."
                        }
                    );
                }
                catch (Exception E)
                {
                    error = E;
                }

                // Assert
                Assert.Null(error);
            }
        }

        [Fact]
        public void GetProject()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                ProjectDTO project = service.GetProject(projectId);

                // Assert
                Assert.NotNull(project);
            }
        }

        [Fact]
        public void DeleteProject()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                service.DeleteProject(projectId);

                KeyNotFoundException error = null;

                try
                {
                    ProjectDTO project = service.GetProject(projectId);
                }
                catch (KeyNotFoundException E)
                {
                    error = E;
                }

                // Assert
                Assert.NotNull(error);
            }
        }*/
    }
}
