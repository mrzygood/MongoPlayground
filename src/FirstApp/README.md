### Hints, lesson-learn

- Always expose port in docker compose.
- Cannot pass Guid as id to mongo because max. characters number is 24.
- When tries to replace document with changed id `MongoWriteException` exception is thrown. 

### Schema rules:
Rule 1: embed unless there is a compelling reason not to  
Rule 2: avoid JOINS if they can be avoided  
Rule 3: array should never grow without bound  
Rule 4: an object should not be embedded if it needs to be accessed individually[1]

TODO:
- Przeanalizuj wykres usa-casów z wideo
- Przejdź kurs na stronie mongo

##### Sources:
[1] https://www.youtube.com/watch?v=QAqK-R9HUhc