ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [TimeTracker], FILENAME = '$(DefaultDataPath)$(DatabaseName)_data.mdf', FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

