(function () {
    'use strict';

    describe('Students service', function () {

        var StudentsService;
        var httpBackend;

        beforeEach(module('common'));
        beforeEach(module('students'));

        beforeEach(inject(function (_StudentsService_, _$httpBackend_) {
            StudentsService = _StudentsService_;
            httpBackend = _$httpBackend_;
        }));

        afterEach(function () {
            httpBackend.flush();
        });

        it('should query for all students', function () {
            httpBackend.whenGET('/api/students/all').respond(200, true);
            StudentsService.getStudents().then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should query for student details by id', function () {
            var studentId = 3;
            httpBackend.whenGET('/api/students/details/' + studentId).respond(200, true);
            StudentsService.getDetails(studentId).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should try to create student', function () {
            var createCommand = {
                firstName: 'John',
                lastName: 'Smith',
                enrollmentDate: new Date(1892, 3, 2)
            };

            httpBackend.whenPOST('/api/students/create', function (data) {
                data = JSON.parse(data);
                expect(data.firstName).toEqual(createCommand.firstName);
                expect(data.lastName).toEqual(createCommand.lastName);
                expect(data.enrollmentDate).toEqual(createCommand.enrollmentDate.toJSON());
                return true;
            }).respond(200, true);

            StudentsService.createStudent(createCommand).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should try to edit student', function () {
            var editCommand = {
                id: 3,
                firstName: 'ModifiedName',
                lastName: 'ModifiedLastName',
                enrollmentDate: new Date(1903, 31, 4)
            };

            httpBackend.whenPOST('/api/students/edit', function (data) {
                data = JSON.parse(data);
                expect(data.id).toEqual(editCommand.id);
                expect(data.firstName).toEqual(editCommand.firstName);
                expect(data.lastName).toEqual(editCommand.lastName);
                expect(data.enrollmentDate).toEqual(editCommand.enrollmentDate.toJSON());
                return true;
            }).respond(200, true);

            StudentsService.editStudent(editCommand).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should try to delete student', function () {
            var studentId = 3;

            httpBackend.whenDELETE('/api/students/delete/' + studentId).respond(206, true);
            StudentsService.deleteStudent(studentId).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should get all available courses', function () {
            httpBackend.whenGET('/api/courses/all').respond(200, true);
            StudentsService.getAvailableCourses().then(function (response) {
                expect(response).toBeTruthy();
            });
        });
    });
})();