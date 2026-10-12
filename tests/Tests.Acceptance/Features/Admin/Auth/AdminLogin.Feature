Feature: Admin Login
as admin, I want to log in using a valid username, password So that I can access the system securely.
   
    Scenario: Login requires all fields
        When they provide credentials phone number: " " and password: " "
        And admin sends a login request
        Then the response status code should be "400"
        
    Scenario: Login fails when phone number does not exist or matches with password
        Given a user with phone number: "090101010101" and password: "password" exists
        When they provide credentials phone number: "09000000000" and password: "password"
        And admin sends a login request
        Then the response status code should be "404"
        And the error message should be "رکوردی پیدا نشد"
        When they provide credentials phone number: "090101010101" and password: "wrong_password"
        Then the response status code should be "404"
        And the error message should be "رکوردی پیدا نشد"
        
    Scenario: Login succeeds when username exists and matches with password
        Given a user with phone number: "09000000000" and password: "password" exists
        When they provide credentials phone number: "09000000000" and password: "password"
        And admin sends a login request
        Then the response status code should be "200"
        And jwt token should not be empty 