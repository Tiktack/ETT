using ETT.Logic.DTO.Projects;
using ETT.Logic.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace ETT.Tests.Projects
{
    public class TaskServiceTest
    {
        /*/// <summary>
        /// Set project id
        /// </summary>
        private const int projectId = 1;

        [Fact]
        public void CreateTask()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                Exception error = null;

                try
                {
                    int number = new Random().Next(1, int.MaxValue);

                    service.CreateTask(
                        new ProjectTaskDTO()
                        {
                            Name = "Task " + number,
                            Description = "Description of the task " + number + ".",
                            ProjectId = projectId,
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
        /// Set inserted task id
        /// </summary>
        private const int taskId = 1;


        [Fact]
        public void UpdateTask()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                Exception error = null;

                try
                {
                    int number = new Random().Next(1, int.MaxValue);

                    service.UpdateTask(
                        new ProjectTaskDTO()
                        {
                            Id = taskId,
                            Name = "Task " + number + " updated",
                            Description = "Description of the updated task " + number + "."
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
        public void GetTask()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                ProjectTaskDTO task = service.GetTask(taskId);

                // Assert
                Assert.NotNull(task);
            }
        }

        [Fact]
        public void GetTasks()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                IEnumerable<ProjectTaskDTO> tasks = service.GetTasks(projectId);

                // Assert
                Assert.NotEmpty(tasks);
            }
        }

        [Fact]
        public void DeleteTask()
        {
            // Arrange
            using (ProjectService service = new ProjectService())
            {
                // Act
                service.DeleteTask(taskId);

                KeyNotFoundException error = null;

                try
                {
                    ProjectTaskDTO project = service.GetTask(taskId);
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
