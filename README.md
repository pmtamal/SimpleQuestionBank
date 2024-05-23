# DsiQuestionBank

## How to run the project

1. Clone the repo from github
2. Go to the repository directory
3. Install docker
4. Open shell or cmd line and run docker-compose up --build
5. Once build is complted and fronend backend and postgress logs are displayed open http://localhost:3000 in the browser
6. Use sysadmin@dsinnovators.com and 12345678 as user name and pasword
7. After login create few user with diffrent roles like quetion_creator reviewer and approver
8. Create categories before creating question
9. Now login as question creator and add new question from myquestion.
10. After submitting the quetion login as reviewer who are setup for that particular category
11. Once reveiwer accept the question it will move to approver
12. After verifying approver will merge and finally question will stored in the bank.
13. If any changes is required approver can send it revision and creator can resolved those comment and same process will be followed to merge the quesion.

Because of dockeriazation few issues might come while running. Let me know and i'll look into it.
