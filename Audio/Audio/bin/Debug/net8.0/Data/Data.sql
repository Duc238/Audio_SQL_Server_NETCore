CREATE DATABASE Audio
GO
USE Audio
GO
CREATE TABLE AudioFiles (
    ID INT PRIMARY KEY IDENTITY,
    FileName NVARCHAR(255),
    AudioData VARBINARY(MAX)
)
GO
INSERT INTO AudioFiles (FileName, AudioData)
VALUES ('AnhSangThienDuongOst-TuVi-4436811.mp3', 
        (SELECT * FROM OPENROWSET(BULK N'E:\Lập trình\.NET core\Audio\Audio\bin\Debug\net8.0\Data\AnhSangThienDuongOst-TuVi-4436811.mp3', SINGLE_BLOB) AS AudioFile))
GO
SELECT FileName, AudioData
FROM AudioFiles
WHERE ID = 1
GO