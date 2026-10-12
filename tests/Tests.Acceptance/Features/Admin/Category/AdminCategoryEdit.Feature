Feature: Admin Category Edit
    As admin i should be able to edit a category with a valid Id
    
    Scenario: A category with provided name already exists
        Given A category with name "test" and image name "test" and Id "3" exists
        And Another category with name: "test2" and image name "test2" and Id "2" exists 
        And Admin provides name: "test" and image name "test" and Id "2" and sends the request 
        Then the response status code should be "409"
        And the error message should be "دسته بندی با عنوان وارد شده وجود دارد."
        
    Scenario: Category with provided Id does not exist
        Given A category with name "test" and image name "test" and Id "3" exists
        And Admin provides name: "testing" and image name "test" and Id "2" and sends the request 
        Then the response status code should be "404"
        And the error message should be "دسته بندی پیدا نشد."
        
    Scenario: Request succeeds and category gets edited
        Given A category with name "test" and image name "test" and Id "3" exists
        And Admin provides name: "testing" and image name "test" and Id "3" and sends the request
        Then the response status code should be "200"
        And the category's name should have changed to "testing"

        

        

        