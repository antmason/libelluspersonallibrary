@echo on
@echo Removing the old empty database...
del emdata
@echo Creating the new one...
call sqlite3.exe -init db.sql emdata .exit
@echo Copying to project directory...
del ..\src\LibrarySolution\PersonalLibrary\Resources\emdata
del /F /Q ..\src\LibrarySolution\PersonalLibrary\Data\*.plb
copy emdata ..\src\LibrarySolution\PersonalLibrary\Resources\
pause