﻿var credentials = null;

module("dashboard", {
    setup: function () {

        S.open("Registration");

        // create new account for testing
        var email = "test_bugs" + new Date().getSeconds() + new Date().getMilliseconds() + "@trackyt.net";
        S('#Email').type(email);
        S('#Password').type(email);
        S('#ConfirmPassword').type(email);

        S('#submit-button').click(function () {
            
            // wait till ajax call finished
            S.wait(1000, function () {
                S('#tasks').exists(function () {
                    credentials = [];
                    credentials.Email = email;
                    credentials.Password = email;
                });
            });
        });
    }
});


// As a user I could create new task
test("create a task", function () {

    // login to dashboard
    S.open("Login", function () {
        S('#Email').type(credentials.Email);
        S('#Password').type(credentials.Password);
        S('#submit-button').click(function () {
            
            // wait till ajax call finished
            S.wait(1000, function () {
            
                // add new task
                S('#task-description').click().type("this is new task");
                S('#add-task').click();

                // wait till task appeared
                S.wait(1000, function () {
                    // get last added task and press start
                    var tasks = S('.task').size();
                    same(tasks, 1, "new task is added and appeared");
                });
            });
        });
    });
});

// As as user I could start my task
test("start a task", function () {

    // login to dashboard
    S.open("Login", function () {
        S('#Email').type(credentials.Email);
        S('#Password').type(credentials.Password);
        S('#submit-button').click(function () {

            // wait till ajax call finished
            S.wait(1000, function () {
                // add new task
                S('#task-description').click().type("this is new task");
                S('#add-task').click();

                // wait till task appeared
                S.wait(1000, function () {

                    // get last added task and press start
                    var tasks = S('.task').size();
                    var index = tasks - 1;

                    S('#start-' + index).click();

                    // wait for 5 seconds
                    S.wait(function () {
                        S('#start-' + index).click();
                        var timer = S('#actualWork-' + index).html();
                        
                        same(timer, "00:05", "5 seconds must be counted by timer");
                    });
                });
            });
        });
    });
});

// As user I could delete my task
// As as user I could start my task
test("delete a task", function () {

    // login to dashboard
    S.open("Login", function () {
        S('#Email').type(credentials.Email);
        S('#Password').type(credentials.Password);
        S('#submit-button').click(function () {

            // wait till ajax call finished
            S.wait(1000, function () {
                // add new task
                S('#task-description').click().type("this is new task");
                S('#add-task').click();

                // wait till task appeared
                S.wait(1000, function () {

                    // get last added task and press start
                    var tasks = S('.task').size();
                    var index = tasks - 1;

                    S('#start-' + index).click();

                    // wait for 5 seconds
                    S.wait(function () {
                        S('#start-' + index).click();

                        // submit it
                        S('#submit').click().wait(1000, function () {

                            S('#delete-' + index).click();

                            // submit my deletion again
                            S('#submit').click().wait(1000, function () {

                                // login to dashboard again
                                S.open("Login", function () {
                                    S('#Email').type(credentials.Email);
                                    S('#Password').type(credentials.Password);
                                    S('#submit-button').click(function () {

                                        // wait till ajax call finished
                                        S.wait(1000, function () {
                                            tasks = S('.task').size();
                                            same(tasks, 0, "all tasks must gone");
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
    });
});