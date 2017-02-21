(function () {
    'use strict';

    describe('Departments service', function () {

        var DepartmentsService;
        var httpBackend;

        beforeEach(module('departments'));

        beforeEach(inject(function (_$httpBackend_, _DepartmentsService_) {
            DepartmentsService = _DepartmentsService_;
            httpBackend = _$httpBackend_;
        }));

        afterEach(function () {
            httpBackend.flush();
        });

        it('should return all departments', function () {

            var departmentsMockList = [
                {
                    id: 1,
                    name: 'Department'
                },
                {
                    id: 2,
                    name: 'Department'
                }
            ];

            httpBackend.whenGET('/api/departments/all').respond(200, { departments: departmentsMockList });

            DepartmentsService.getDepartments().then(function (response) {
                expect(response.data.departments).toBeDefined();
            });
        });

        it('should query for details by id', function () {

            var department = {
                id: 1,
                name: 'Department'
            };

            httpBackend.whenGET('/api/departments/details/1').respond(200, { department: department });

            DepartmentsService.getDetails(1).then(function (response) {
                expect(response.data.department).toBeDefined();
            });
        });

        it('should post to create department', function () {
            var department = {
                name: 'Department'
            };

            httpBackend.whenPOST('/api/departments/create', function (data) {
                data = JSON.parse(data);
                expect(data.name).toEqual(department.name);
                return true;
            }).respond(200, true);

            DepartmentsService.addDepartment(department).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should try to delete department', function () {

            var departmentIdToDelete = 1;

            httpBackend.expectDELETE('/api/departments/delete/' + departmentIdToDelete).respond(204, true);

            DepartmentsService.deleteDepartment(departmentIdToDelete).then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should query for available instructors', function () {

            httpBackend.whenGET('/api/instructors/all').respond(200, true);

            DepartmentsService.getAvailableInstructors().then(function (response) {
                expect(response).toBeTruthy();
            });
        });

        it('should try to edit department', function () {

            var editCommand = {
                name: "New department name"
            };

            httpBackend.whenPOST('/api/departments/edit', function (data) {
                data = JSON.parse(data);
                expect(data.name).toEqual(editCommand.name);
                return true;
            }).respond(200, true);

            DepartmentsService.editDepartment(editCommand).then(function (response) {
                expect(response).toBeTruthy();
            });
        });
    });
})();