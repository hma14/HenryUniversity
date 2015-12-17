using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HenryUniversity.Controllers;
using HenryUniversity.Models;
using HenryUniversity.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Web.Http;
using HenryUniversity.DAL;
using Moq;
using System.Data.Entity;

namespace HenryUniversity.UnitTests
{
    [TestClass]
    public class TestCoursesController
    {
        [TestMethod]
        public void GetAllCourses()
        {
            var context = new TestSchoolContext();
            context.Courses.Add(new Course
            {
                CourseID = 4,
                Title = "English Literature 4",
                Credits = 3,
                DepartmentID = 1600
            });
            context.Courses.Add(new Course
            {
                CourseID = 5,
                Title = "English Literature 5",
                Credits = 3,
                DepartmentID = 1600
            });
            context.Courses.Add(new Course
            {
                CourseID = 5,
                Title = "English Literature 5",
                Credits = 3,
                DepartmentID = 1600
            });

            context.Departments.Add(new Department
            {
                DepartmentID = 1600,
                Name = "English",
                Budget = 350000.00M,
                StartDate = DateTime.Now.AddYears(-5)
            });

            var controller = new CoursesController(context);
            var result = (IQueryable<CourseDto>)controller.GetCourses();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.First().CourseID);
            Assert.AreEqual("English Literature 4", result.First().Title);
            Assert.AreEqual("English", result.First().DepartmentName);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetSingleCourse()
        {
            var context = new TestSchoolContext();
            var course = getDemoCourse();
            context.Courses.Add(course);
            var controller = new CoursesController(context);
            var result = await controller.GetCourse(4) as OkNegotiatedContentResult<Course>;

            Assert.IsNotNull(result);
            //Assert.AreEqual(4, result.Content.CourseID);
        }



        [TestMethod]
        public async Task GetSingleCourseMoq()
        {
            var contextMock = new Mock<DbContextMock>();

            contextMock.Setup
                (
                    m => ((DbSet<Course>)m.Courses).FindAsync
                    (
                        It.IsAny<int>()
                    )
                )
                .Returns
                (
                    Task.FromResult(new Course
                    {
                        CourseID = 4,
                        Title = "English Literature 4",
                        Credits = 3,
                        DepartmentID = 1600
                    }
                ));


            var controller = new CoursesController(contextMock.Object);
            var result = await controller.GetCourse(4) as OkNegotiatedContentResult<Course>;

            contextMock.Verify(
                m => ((DbSet) m.Courses).FindAsync(It.Is<int>(x => x == 4)), Times.Exactly(1));
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Content.CourseID);
        }

        [TestMethod]
        public void GetAllCoursesMoq()
        {
            var contextMock = new Mock<DbContextMock>();

            var data = getListCourse();
            var dbSetMock = new Mock<IDbSet<Course>>();
            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            contextMock.Setup
                (
                    m => m.Courses
                )
                .Returns
                (
                    dbSetMock.Object
                );

            var departments = getListDepartment();
            var dbSetMockDepartment = new Mock<IDbSet<Department>>();
            dbSetMockDepartment.Setup(m => m.Provider).Returns(departments.Provider);
            dbSetMockDepartment.Setup(m => m.Expression).Returns(departments.Expression);
            dbSetMockDepartment.Setup(m => m.ElementType).Returns(departments.ElementType);
            dbSetMockDepartment.Setup(m => m.GetEnumerator()).Returns(departments.GetEnumerator());

            contextMock.Setup
                (
                    m => m.Departments

                )
                .Returns
                (
                    dbSetMockDepartment.Object
                );



            var controller = new CoursesController(contextMock.Object);
            var result = controller.GetCourses() as OkNegotiatedContentResult<Course>;

            //contextMock.Verify(
            //    m => m.Courses.FindAsync(It.Is<int>(x => x == 4)), Times.Exactly(1));
            Assert.IsNotNull(result);
            //Assert.AreEqual(4, result.Content.CourseID);
        }


        IQueryable<Course> getListCourse()
        {
            List<Course> lst = new List<Course>
            {
                new Course
                {
                    CourseID = 4,
                    Title = "English Literature 4",
                    Credits = 3,
                    DepartmentID = 1600
                },
                new Course
                {
                    CourseID = 5,
                    Title = "English Literature 5",
                    Credits = 3,
                    DepartmentID = 1600
                },

                new Course
                {
                    CourseID = 5,
                    Title = "English Literature 5",
                    Credits = 3,
                    DepartmentID = 1600
                }
            };
            return lst.AsQueryable();
        }

        IQueryable<Department> getListDepartment()
        {
            var lst = new List<Department>
            {
                new Department {
                    DepartmentID = 1600,
                    Name = "English",
                    Budget = 350000.00M,
                    StartDate = DateTime.Now.AddYears(-5)
                }
            };
            return lst.AsQueryable();
        }


        Course getDemoCourse()
        {
            return new Course()
            {
                CourseID = 4,
                Title = "English Literature 4",
                Credits = 3,
                DepartmentID = 1600
            };
        }
    }
}
