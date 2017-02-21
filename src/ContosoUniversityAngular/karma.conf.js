// Karma configuration
// Generated on Wed Feb 01 2017 11:16:38 GMT+0200 (FLE Standard Time)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: './wwwroot',


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
    	// Dependencies
    	'./lib/angular/angular.js',
    	'./lib/angular-mocks/angular-mocks.js',
    	'./lib/angular-toastr/dist/angular-toastr.js',
    	'./lib/angular-ui-router/release/angular-ui-router.js',
    	// Directives
    	'./App/Directives/Directives.js',
        './App/Directives/CatchValidationErrorsDirective.js',
        './App/Tests/Directives/CatchValidationErrorsDirective.spec.js',
    	// App
    	'./App/App.js',
    	'./App/ErrorsService.js',
        './App/NotificationsService.js',
        './App/Tests/Common/ErrorsService.spec.js',
        './App/Tests/Common/NotificationsService.spec.js',
    	// Courses
    	'./App/Courses/App.js',
    	'./App/Courses/Services/CoursesService.js',
    	'./App/Courses/Controllers/ListController.js',
    	'./App/Tests/Courses/CoursesService.spec.js',
    	'./App/Tests/Courses/ListController.spec.js',
    	// Departments
    	'./App/Departments/App.js',
    	'./App/Departments/Services/DepartmentsService.js',
    	'./App/Tests/Departments/DepartmentsService.spec.js',
    	// Instructors
    	'./App/Instructors/App.js',
    	'./App/Instructors/Services/InstructorsService.js',
    	'./App/Tests/Instructors/InstructorsService.spec.js',
    	// Students
    	'./App/Students/App.js',
    	'./App/Students/Services/StudentsService.js',
    	'./App/Tests/Students/StudentsService.spec.js'
    ],


    // list of files to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['spec'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: ['Chrome'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false,

    // Concurrency level
    // how many browser should be started simultaneous
    concurrency: Infinity
  })
}
