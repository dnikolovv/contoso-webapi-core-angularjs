(function () {
    'use strict';

    describe('Courses service', function () {

        var CoursesService;
        var httpBackend;

        beforeEach(module('courses'));

        beforeEach(inject(function (_$httpBackend_, _CoursesService_) {
            CoursesService = _CoursesService_;
            httpBackend = _$httpBackend_;
        }));

        afterEach(function () {
            httpBackend.flush();
        });

        it('should return all courses', function () {

            httpBackend.whenGET('/api/courses/all').respond(200, true);

            CoursesService.getCourses().then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should query for details by id', function () {

            httpBackend.whenGET('/api/courses/details/1').respond(200, true);

            CoursesService.getDetails(1).then(function (response) {
               expect(response).toBeTruthy();
            });
        });

        it('should post to create course', function () {
           var course = {
               title: 'Some course title',
               credits: 4,
               department: {
                   name: 'Department'
               }
           };

           httpBackend.whenPOST('/api/courses/create', function (data) {
               data = JSON.parse(data);
               expect(data.title).toEqual(course.title);
               expect(data.credits).toEqual(course.credits);
               expect(data.department.name).toEqual(course.department.name);
               return true;
           }).respond(200, true);

           CoursesService.createCourse(course).then(function (response) {
              expect(response).toBeTruthy();
           });
        });

        it('should try to delete course', function () {

            var courseIdToDelete = 1;

            httpBackend.expectDELETE('/api/courses/delete/' + courseIdToDelete).respond(204, true);

            CoursesService.deleteCourse(courseIdToDelete).then(function (response) {
               expect(response).toBeTruthy();
            });
        });

        it('should query for available departments', function () {

            httpBackend.whenGET('/api/departments/all').respond(200, true);

            CoursesService.getAvailableDepartments().then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should try to edit course', function () {

            var editCommand = {
                title: "New course title",
                credits: 2
            };

            httpBackend.whenPOST('/api/courses/edit', function (data) {
                data = JSON.parse(data);
                expect(data.title).toEqual(editCommand.title);
                expect(data.credits).toEqual(editCommand.credits);
                return true;
            }).respond(200, true);

            CoursesService.editCourse(editCommand).then(function (response) {
                expect(response).toBeTruthy();
            });
        });
    });
})();