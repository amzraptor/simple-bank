**To Run Tests**

execute the following at the root folder:
```
dotnet test
```
for more info:
```
dotnet test -h
```

**Assumptions**
- Datetime is not passed as a parameter for simplicity-sake. Running the program at 11:59PM has a low chance of yielding bad results around daily withdrawal limit functionality.
- Validation around Transaction Request amounts is not implemented, so avoid using negative amounts. For example, a withdrawal request & deposit request are both expected to have positive values for the amount.
- A transfer is treated as a withdrawal then a deposit. Else, one can transfer the money to an account that doesn't have a withdrawal limit to overcome the limit.
- The amount is calculated based on the transactions, functionality can be extend to view balance at a specific date.

