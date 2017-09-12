CREATE TABLE cw_syspara (
  ParaName VARCHAR(50) NOT NULL DEFAULT '',
  ParaType VARCHAR(200) DEFAULT NULL,
  ParaValue TEXT COLLATE gbk_chinese_ci,
  DefValue TEXT COLLATE gbk_chinese_ci,
  DefPara1 VARCHAR(200) DEFAULT NULL,
  DefPara2 VARCHAR(200) DEFAULT NULL,
  DefPara3 VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ParaName)

)ENGINE=MyISAM;

CREATE TABLE cw_logs (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  LogContent TEXT COLLATE gbk_chinese_ci,
  LogUser VARCHAR(200) DEFAULT NULL,
  LogName VARCHAR(200) DEFAULT NULL,
  LogUID VARCHAR(200) DEFAULT NULL,
  LogPID VARCHAR(200) DEFAULT NULL,
  LogTime DATETIME DEFAULT NULL,
  LogDefine1 VARCHAR(200) DEFAULT NULL,
  LogDefine2 VARCHAR(200) DEFAULT NULL,
  LogDefine3 VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_backupdb (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  BackupPath VARCHAR(200) DEFAULT NULL,
  Notes TEXT COLLATE gbk_chinese_ci,
  BackupDate DATETIME DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_account (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  QueryID VARCHAR(160) DEFAULT NULL,
  QueryNo varchar(50) default NULL,
  LevelID VARCHAR(32) DEFAULT NULL,
  AccountNo VARCHAR(50) DEFAULT NULL,
  AccountName VARCHAR(50) DEFAULT NULL,
  AccountDate VARCHAR(16) DEFAULT NULL,
  AccountYear VARCHAR(100) DEFAULT NULL,
  StartAccountDate VARCHAR(16) DEFAULT NULL,
  LastCarryDate VARCHAR(16) DEFAULT NULL,
  RealLastCarry VARCHAR(16) DEFAULT NULL,
  YearCarryVoucher VARCHAR(32) DEFAULT NULL,
  YearCarryFlag VARCHAR(32) DEFAULT NULL,
  Director VARCHAR(50) DEFAULT NULL,
  SessionID VARCHAR(50) DEFAULT NULL,
  CTLDateTime DATETIME DEFAULT NULL,
  CreateDateTime DATETIME DEFAULT NULL,
  IsLock VARCHAR(20) DEFAULT NULL,
  Define1 VARCHAR(100) DEFAULT NULL,
  Define2 VARCHAR(100) DEFAULT NULL,
  Define3 VARCHAR(100) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_collectlevel (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  LevelName VARCHAR(100) DEFAULT NULL,
  CollectParent TEXT COLLATE gbk_chinese_ci,
  CollectUnits TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_collectrowitem (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  LevelID VARCHAR(32) DEFAULT NULL,
  DesignID VARCHAR(32) DEFAULT NULL,
  ItemName0 VARCHAR(200) DEFAULT NULL,
  RowNo0 VARCHAR(10) DEFAULT NULL,
  ItemExpr0 TEXT COLLATE gbk_chinese_ci,
  ItemExpr1 TEXT COLLATE gbk_chinese_ci,
  ItemName1 VARCHAR(200) DEFAULT NULL,
  RowNo1 VARCHAR(10) DEFAULT NULL,
  ItemExpr2 TEXT COLLATE gbk_chinese_ci,
  ItemExpr3 TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_definereport (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  ReportName VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_definerowitem (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  LevelID VARCHAR(32) DEFAULT NULL,
  DefineID VARCHAR(32) DEFAULT NULL,
  ItemName0 VARCHAR(200) DEFAULT NULL,
  RowNo0 VARCHAR(10) DEFAULT NULL,
  ItemExpr0 TEXT COLLATE gbk_chinese_ci,
  ItemExpr1 TEXT COLLATE gbk_chinese_ci,
  ItemName1 VARCHAR(200) DEFAULT NULL,
  RowNo1 VARCHAR(10) DEFAULT NULL,
  ItemExpr2 TEXT COLLATE gbk_chinese_ci,
  ItemExpr3 TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_recordid (
  TableName VARCHAR(50) NOT NULL DEFAULT '',
  LastID VARCHAR(32) DEFAULT NULL,
  PRIMARY KEY (TableName)

)ENGINE=MyISAM;

CREATE TABLE cw_reportdesign (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  TableName VARCHAR(100) DEFAULT NULL,
  HeadString TEXT COLLATE gbk_chinese_ci,
  GatherCells TEXT COLLATE gbk_chinese_ci,
  ColumnWidth TEXT COLLATE gbk_chinese_ci,
  HAlign VARCHAR(99) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_reportrowitem (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  DesignID VARCHAR(32) DEFAULT NULL,
  RowInfo TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_reportsignname (
  UnitID VARCHAR(6) NOT NULL DEFAULT '',
  ReportSignName TEXT COLLATE gbk_chinese_ci,
  QueryID varchar(200) default NULL,
  QueryNo varchar(50) default NULL,
  PRIMARY KEY (UnitID)

)ENGINE=MyISAM;

CREATE TABLE cw_subject (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  ParentNo VARCHAR(60) DEFAULT NULL,
  SubjectNo VARCHAR(60) DEFAULT NULL,
  SubjectName VARCHAR(200) DEFAULT NULL,
  SubjectType VARCHAR(1) DEFAULT NULL,
  AccountType VARCHAR(1) DEFAULT NULL,
  BeginBalance DOUBLE DEFAULT NULL,
  IsEntryData VARCHAR(1) DEFAULT NULL,
  IsDetail VARCHAR(1) DEFAULT NULL,
  AccountStruct VARCHAR(1) DEFAULT NULL,
  AccountFlag VARCHAR(1) DEFAULT NULL,
  IsLock VARCHAR(1) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_users (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  RealName VARCHAR(30) DEFAULT NULL,
  UserName VARCHAR(30) DEFAULT NULL,
  Password VARCHAR(30) DEFAULT NULL,
  Powers TEXT COLLATE gbk_chinese_ci,
  UserType VARCHAR(2) DEFAULT NULL,
  AccountID TEXT COLLATE gbk_chinese_ci,
  LastLogin DATETIME DEFAULT NULL,
  LoginCounts INTEGER(11) DEFAULT 0,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_indexmonitor (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  IndexSubject VARCHAR(200) DEFAULT NULL,
  IndexType VARCHAR(20) DEFAULT NULL,
  IndexValue DOUBLE(15,3) DEFAULT NULL,
  IndexState VARCHAR(20) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_resclass (
  ID VARCHAR(160) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  ParentID VARCHAR(160) DEFAULT NULL,
  ClassName VARCHAR(100) DEFAULT NULL,
  LinkSubject VARCHAR(120) DEFAULT NULL,
  Measures VARCHAR(20) DEFAULT NULL,
  Notes TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_balancealarm (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  UnitID VARCHAR(32) DEFAULT NULL,
  AccountID VARCHAR(32) DEFAULT NULL,
  VoucherID VARCHAR(32) DEFAULT NULL,
  VoucherNo VARCHAR(20) DEFAULT NULL,
  VoucherDate VARCHAR(20) DEFAULT NULL,
  AlarmType VARCHAR(20) DEFAULT NULL,
  DoState VARCHAR(20) DEFAULT NULL,
  BookTime DATETIME DEFAULT NULL,
  Define1 VARCHAR(200) DEFAULT NULL,
  Define2 VARCHAR(200) DEFAULT NULL,
  Define3 VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL auto_increment,
  `TitleID` int(11) default '0' COMMENT '职务ID',
  `TitleName` char(50) default NULL COMMENT '务职名称',
  `DeptID` int(11) default '0' COMMENT '部门ID',
  `DeptName` char(100) default NULL COMMENT '部门名称',
  `TrueName` char(50) default NULL COMMENT '真实姓名',
  `BarCode` char(100) default NULL COMMENT '条形码',
  `SubTime` int(11) default NULL COMMENT '提交时间',
  `SubUID` int(11) default NULL COMMENT '提交人UID',
  `DelFlag` int(11) default '0' COMMENT '删除标记',
  `telphone` char(100) default NULL COMMENT '提交人UID',
  `address` char(100) default '0' COMMENT '删除标记',
  PRIMARY KEY  (`UserID`)
) ENGINE=MyISAM;

CREATE TABLE `usertitle` (
  `TitleID` int(11) NOT NULL auto_increment,
  `TitleName` char(50) default NULL,
  `TitleDesc` char(100) default NULL,
  `SubTime` int(11) default NULL COMMENT '交提时间',
  `SubUID` int(11) default NULL COMMENT '提交人',
  `DelFlag` tinyint(5) default '0',
  PRIMARY KEY  (`TitleID`)
) ENGINE=MyISAM;

CREATE TABLE `contidion` (
  `ID` int(11) NOT NULL auto_increment,
  `Step1` double default '0',
  `Step2` double default '0',
  `DeptID` int(11) default '0' COMMENT '部门ID',
  `DelFlag` tinyint(4) default '0' COMMENT '删除标记',
  `SubTime` int(11) default '0' COMMENT '提交时间',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=gb2312;

CREATE TABLE `ceping` (
  `ID` int(11) NOT NULL auto_increment,
  `DeptID` int(11) default '0',
  `DeptName` varchar(50) default NULL,
  `Evaluation` varchar(100) default NULL COMMENT '综合评价',
  `SubUID` int(11) default '0' COMMENT '提交人UID',
  `SubIP` char(50) default NULL COMMENT '提交IP',
  `SubTime` int(11) default NULL COMMENT '提交时间',
  `OptionChecked` tinyint(4) default '0' COMMENT '评价选项 1:满意 2：基本满意 3：不满意',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `tousu` (
  `ID` int(11) NOT NULL auto_increment,
  `DeptID` int(11) default '0' COMMENT '投诉单位ID',
  `DeptName` char(30) default NULL COMMENT '部门名称',
  `Content` text COMMENT '投诉内容',
  `SubUID` int(11) default '0' COMMENT '提交人UID',
  `SubIP` char(50) default NULL COMMENT '提交IP',
  `SubTime` char(50) default '0' COMMENT '提交时间',
  `DelFlag` tinyint(4) default '0' COMMENT '删除标记',
  `TFlag` int(11) default '0' COMMENT '提交人UID',

  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `cwgk_lbb` (
  `id` int(11) NOT NULL auto_increment,
  `name` varchar(1000) default NULL,
  `filename` varchar(1000) default NULL,
  `creattime` varchar(100) default NULL,
  `lbid` varchar(100) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM;

CREATE TABLE `projects` (
  `ID` varchar(100) NOT NULL,
  `TownID` varchar(100) default NULL,
  `AccountID` varchar(100) default NULL,
  `ProjectName` varchar(200) default NULL,
  `ProjectType` varchar(100) default NULL,
  `BiaoDiMoeny` double(15,3) default '0.000',
  `BidMoney` double(15,3) default '0.000',
  `BidDate` varchar(20) default NULL,
  `SDate` varchar(20) default NULL,
  `EDate` varchar(20) default NULL,
  `Booker` varchar(20) default NULL,
  `BookTime` varchar(20) default NULL,
  `ZiChan` mediumtext,
  `ZiYuan` mediumtext,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `projattachs` (
  `ID` int(11) NOT NULL auto_increment,
  `ProjectID` varchar(100) default NULL,
  `StepFlag` varchar(20) default NULL,
  `FileName` varchar(200) default NULL,
  `FilePath` varchar(200) default NULL,
  `UploadTime` varchar(20) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;