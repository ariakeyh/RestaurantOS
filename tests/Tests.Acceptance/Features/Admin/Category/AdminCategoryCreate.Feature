Feature: Admin Category Create
    As admin i should be able to create a category with a valid name
    
    Scenario: Category's name must be unique
        Given A category with name "name" and image name "image name" exists
        When Admin provides name: "name" and image name: "image name"
        And Admin sends the create category request
        Then the response status code should be "409"
        And the error message should be "دسته بندی با عنوان وارد شده وجود دارد."
        
    Scenario: Category creation requires field 'Name' to not be empty
        When Admin provides name: " " and image name: "image name"
        And Admin sends the create category request
        Then the response status code should be "400"
        
    Scenario: Category creation is successful
        When Admin provides name: "name" and image name: "image name"
        And Admin sends the create category request
        Then the response status code should be "200"
        And A category with name "name" and image name: "image name" should exist in the database