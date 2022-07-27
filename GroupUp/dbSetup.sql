CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS _groups (
  id INT NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  picture varchar(255) COMMENT 'User Picture',
  isPrivate TINYINT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE

)default charset utf8 COMMENT '';


/* MANY TO MANY GROUP MEMBER*/

CREATE TABLE IF NOT EXISTS groupmembers (
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  groupId INT NOT NULL,
  accountId VARCHAR(255) NOT NULL,

  FOREIGN KEY (accountId)
    REFERENCES accounts(id)
    ON DELETE CASCADE,

  FOREIGN KEY (groupId)
   REFERENCES _groups(id)
   ON DELETE CASCADE
)default charset utf8 COMMENT '';
