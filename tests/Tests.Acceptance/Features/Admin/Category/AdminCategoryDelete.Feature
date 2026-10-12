Feature: Admin Category Delete
    As admin I should be able to delete a category with a valid Id
    
    Scenario: Category with provided Id doesn't exist
        Given A category with Id of 67 exists
        When Admin provides Id 6 and sends the request
        Then the response status code should be "404"
        And the error message should be "دسته بندی پیدا نشد."
        
    Scenario: Request succeeds and category gets soft deleted
        Given A category with Id of 67 exists
        When Admin provides Id 67 and sends the request
        Then the response status code should be "200"
        And category with Id 67 should be deleted