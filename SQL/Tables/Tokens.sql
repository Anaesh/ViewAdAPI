use ViewAd_BMES;
CREATE TABLE IF NOT EXISTS Tokens (
    Id VARCHAR(36),
    CurrentRewardValue INT,
    Image VARCHAR(2000),
    IsActive BIT,
    MinimumWithdrawl INT,
    Symbol VARCHAR(255),
    TokenId INT,
    TokenName VARCHAR(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
	UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT PK_Tokens_Id PRIMARY KEY (Id),
    CONSTRAINT UK_Tokens_TokenId UNIQUE (TokenId),
    CONSTRAINT UK_Tokens_TokenName UNIQUE (TokenName)
);