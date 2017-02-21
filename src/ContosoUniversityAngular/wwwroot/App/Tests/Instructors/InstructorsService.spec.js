(function () {
    'use strict';

    describe('Instructors service', function () {

        var InstructorsService;
        var httpBackend;

        beforeEach(module('common'));
        beforeEach(module('instructors'));

        beforeEach(inject(function (_InstructorsService_, _$httpBackend_) {
            InstructorsService = _InstructorsService_;
            httpBackend = _$httpBackend_;
        }));

        afterEach(function () {
            httpBackend.flush();
        });

        it('should query for instructors', function () {
            httpBackend.whenGET('/api/instructors/all').respond(200, true);

            InstructorsService.getInstructors().then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should query for instructor details by id', function () {
            var instructorId = 1;
            httpBackend.whenGET('/api/instructors/details/' + instructorId).respond(200, true);

            InstructorsService.getDetails(instructorId).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should post to create instructor', function () {
            var createCommand = {
                firstName: 'John',
                lastName: 'Smith',
                dateHired: new Date(1993, 2, 3)
            };

            httpBackend.whenPOST('/api/instructors/create', function (data) {
                data = JSON.parse(data);
                expect(data.firstName).toEqual(createCommand.firstName);
                expect(data.lastName).toEqual(createCommand.lastName);
                expect(data.dateHired).toEqual(createCommand.dateHired.toJSON());
                return true;
            }).respond(200, true);

            InstructorsService.createInstructor(createCommand).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should post to edit instructor', function () {
            var editCommand = {
                id: 3,
                firstName: 'Smith',
                lastName: 'John'
            };

            httpBackend.whenPOST('/api/instructors/edit', function (data) {
                data = JSON.parse(data);
                expect(data.id).toEqual(editCommand.id);
                expect(data.firstName).toEqual(editCommand.firstName);
                expect(data.lastName).toEqual(editCommand.lastName);
                return true;
            }).respond(200, true);

            InstructorsService.editInstructor(editCommand).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should try to delete instructor', function () {
            var instructorId = 3;

            httpBackend.whenDELETE('/api/instructors/delete/' + instructorId)
                .respond(206, true);

            InstructorsService.deleteInstructor(instructorId).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should query for available courses', function () {
            httpBackend.whenGET('/api/courses/all').respond(200, true);

            InstructorsService.getAvailableCourses().then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should query for course details', function () {
            var courseId = 5;
            httpBackend.whenGET('/api/courses/details/' + courseId).respond(200, true);

            InstructorsService.getCourseDetails(courseId).then(function (response) {
                expect(response).toBeTruthy();
            });
        });
    });
})();