CREATE TABLE cw_recordid (
  TableName VARCHAR(50) NOT NULL DEFAULT '',
  LastID VARCHAR(32) DEFAULT NULL,
  PRIMARY KEY (TableName)

)ENGINE=MyISAM;

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

CREATE TABLE cw_summary (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  Contents TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_backupdb (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  BackupPath VARCHAR(200) DEFAULT NULL,
  Notes TEXT COLLATE gbk_chinese_ci,
  BackupDate DATETIME DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_department (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  PID VARCHAR(32) DEFAULT NULL,
  DeptName VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_assetclass (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  PID VARCHAR(160) DEFAULT NULL,
  CName VARCHAR(200) DEFAULT NULL,
  UseLife VARCHAR(20) DEFAULT NULL,
  SVRate VARCHAR(20) DEFAULT NULL,
  MUnit VARCHAR(20) DEFAULT NULL,
  DeprMethod VARCHAR(20) DEFAULT NULL,
  LinkSubject VARCHAR(200) DEFAULT NULL,
  DeprSubject VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_ditype (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  TName VARCHAR(200) DEFAULT NULL,
  TType VARCHAR(20) DEFAULT NULL,
  LinkSubject VARCHAR(20) DEFAULT NULL,
  NValueSubject VARCHAR(20) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_assetcard (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  CardID VARCHAR(32) DEFAULT NULL,
  AssetNo VARCHAR(32) DEFAULT NULL,
  AssetName VARCHAR(200) DEFAULT NULL,
  ClassID VARCHAR(32) DEFAULT NULL,
  CName VARCHAR(200) DEFAULT NULL,
  AssetModel VARCHAR(200) DEFAULT NULL,
  DeptName VARCHAR(200) DEFAULT NULL,
  AddType VARCHAR(20) DEFAULT NULL,
  Depositary VARCHAR(200) DEFAULT NULL,
  UseState VARCHAR(20) DEFAULT NULL,
  UseLife VARCHAR(20) DEFAULT NULL,
  DeprMethod VARCHAR(20) DEFAULT NULL,
  SUseDate VARCHAR(16) DEFAULT NULL,
  UsedMonths DOUBLE(32,6) DEFAULT 0,
  CurrencyType VARCHAR(20) DEFAULT NULL,
  OldPrice DOUBLE(32,6) DEFAULT 0,
  OldPrice0 DOUBLE(32,6) DEFAULT 0,
  JingCZL DOUBLE(32,6) DEFAULT 0,
  JingCZ DOUBLE(32,6) DEFAULT 0,
  ZheJiu DOUBLE(32,6) DEFAULT 0,
  ZheJiu0 DOUBLE(32,6) DEFAULT 0,
  MonthZJL DOUBLE(32,6) DEFAULT 0,
  MonthZJE DOUBLE(32,6) DEFAULT 0,
  NewPrice DOUBLE(32,6) DEFAULT 0,
  ThisZJ DOUBLE(32,6) DEFAULT 0,
  DeprSubject VARCHAR(200) DEFAULT NULL,
  AssetItem VARCHAR(200) DEFAULT NULL,
  AUnit VARCHAR(200) DEFAULT NULL,
  AAmount DOUBLE(32,6) DEFAULT 0,
  HasAmount DOUBLE(32,6) DEFAULT 0,
  APrice DOUBLE(32,6) DEFAULT 0,
  APicture VARCHAR(200) DEFAULT NULL,
  AssetAdmin VARCHAR(20) DEFAULT NULL,
  Notes TEXT COLLATE gbk_chinese_ci,
  DeprState VARCHAR(20) DEFAULT NULL,
  AssetState VARCHAR(20) DEFAULT NULL,
  BookTime VARCHAR(20) DEFAULT NULL,
  BookDate VARCHAR(20) DEFAULT NULL,
  BookMan VARCHAR(20) DEFAULT NULL,
  CVoucher VARCHAR(20) DEFAULT NULL,
  CDate DATETIME DEFAULT NULL,
  CType VARCHAR(200) DEFAULT NULL,
  LSubject VARCHAR(200) DEFAULT NULL,
  CRSubject VARCHAR(200) DEFAULT NULL,
  CRMoney DOUBLE(32,6) DEFAULT 0,
  CCSubject VARCHAR(200) DEFAULT NULL,
  CCMoney DOUBLE(32,6) DEFAULT 0,
  CNotes TEXT COLLATE gbk_chinese_ci,
  CleanDate VARCHAR(20) DEFAULT NULL,
  RemindFlag VARCHAR(20) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_assetda (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  CardID VARCHAR(32) DEFAULT NULL,
  ClassID VARCHAR(32) DEFAULT NULL,
  VoucherID VARCHAR(32) DEFAULT NULL,
  VoucherDate VARCHAR(16) DEFAULT NULL,
  VSummary VARCHAR(200) DEFAULT NULL,
  OldPrice0 DOUBLE(32,6) DEFAULT NULL,
  OldPrice1 DOUBLE(32,6) DEFAULT NULL,
  ZheJiu0 DOUBLE(32,6) DEFAULT NULL,
  ZheJiu1 DOUBLE(32,6) DEFAULT NULL,
  ThisZheJiu DOUBLE(32,6) DEFAULT NULL,
  Define1 VARCHAR(200) DEFAULT NULL,
  Define2 VARCHAR(200) DEFAULT NULL,
  Define3 VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_assetmodify (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  ItemName VARCHAR(100) DEFAULT NULL,
  ItemValueOld VARCHAR(200) DEFAULT NULL,
  ItemValueNew VARCHAR(200) DEFAULT NULL,
  ItemContent VARCHAR(500) DEFAULT NULL,
  ModifyUser VARCHAR(20) DEFAULT NULL,
  ModifyTime DATETIME DEFAULT NULL,
  Define1 VARCHAR(200) DEFAULT NULL,
  Define2 VARCHAR(200) DEFAULT NULL,
  Define3 VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_subject (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  ParentNo VARCHAR(60) DEFAULT NULL,
  SubjectNo VARCHAR(60) DEFAULT NULL,
  SubjectName VARCHAR(200) DEFAULT NULL,
  SubjectType VARCHAR(1) DEFAULT NULL,
  AccountType VARCHAR(1) DEFAULT NULL,
  BeginBalance DOUBLE(32,6) DEFAULT 0,
  IsEntryData VARCHAR(1) DEFAULT NULL,
  IsDetail VARCHAR(1) DEFAULT NULL,
  AccountStruct VARCHAR(1) DEFAULT NULL,
  AccountFlag VARCHAR(1) DEFAULT NULL,
  IsLock VARCHAR(1) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_subjectrec (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  ParentNo VARCHAR(60) DEFAULT NULL,
  SubjectNo VARCHAR(60) DEFAULT NULL,
  SubjectName VARCHAR(200) DEFAULT NULL,
  SubjectType VARCHAR(1) DEFAULT NULL,
  AccountType VARCHAR(1) DEFAULT NULL,
  BeginBalance DOUBLE(32,6) DEFAULT 0,
  IsEntryData VARCHAR(1) DEFAULT NULL,
  IsDetail VARCHAR(1) DEFAULT NULL,
  AccountStruct VARCHAR(1) DEFAULT NULL,
  AccountFlag VARCHAR(1) DEFAULT NULL,
  IsLock VARCHAR(1) DEFAULT NULL,
  YearMonth VARCHAR(16) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_subjectdata (
  SubjectID VARCHAR(32) NOT NULL DEFAULT '',
  Price DOUBLE(32,6) DEFAULT 0,
  Amount DOUBLE(32,6) DEFAULT 0,
  Balance DOUBLE(32,6) DEFAULT 0,
  SUnit VARCHAR(200) DEFAULT NULL,
  SType VARCHAR(200) DEFAULT NULL,
  SClass VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (SubjectID)

)ENGINE=MyISAM;

CREATE TABLE cw_subjectgroup (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  GroupName VARCHAR(50) DEFAULT NULL,
  SubjectNo TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_subjectsum (
  ID INTEGER(11) NOT NULL AUTO_INCREMENT,
  SubjectID VARCHAR(32) DEFAULT NULL,
  SubjectNo VARCHAR(60) DEFAULT NULL,
  SubjectName VARCHAR(200) DEFAULT NULL,
  YearMonth VARCHAR(16) DEFAULT NULL,
  BeginBalance DOUBLE(32,6) DEFAULT 0,
  Lead DOUBLE(32,6) DEFAULT 0,
  Onloan DOUBLE(32,6) DEFAULT 0,
  LeadSum DOUBLE(32,6) DEFAULT 0,
  OnloanSum DOUBLE(32,6) DEFAULT 0,
  FinalBalance DOUBLE(32,6) DEFAULT 0,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_amountsum (
  ID INTEGER(11) NOT NULL AUTO_INCREMENT,
  SubjectID VARCHAR(32) DEFAULT NULL,
  SubjectNo VARCHAR(60) DEFAULT NULL,
  SubjectName VARCHAR(200) DEFAULT NULL,
  YearMonth VARCHAR(16) DEFAULT NULL,
  BeginBalanceA DOUBLE(32,6) DEFAULT 0,
  LeadA DOUBLE(32,6) DEFAULT 0,
  OnloanA DOUBLE(32,6) DEFAULT 0,
  LeadSumA DOUBLE(32,6) DEFAULT 0,
  OnloanSumA DOUBLE(32,6) DEFAULT 0,
  FinalBalanceA DOUBLE(32,6) DEFAULT 0,
  BeginBalanceP DOUBLE(32,6) DEFAULT 0,
  Lead DOUBLE(32,6) DEFAULT 0,
  Onloan DOUBLE(32,6) DEFAULT 0,
  LeadSum DOUBLE(32,6) DEFAULT 0,
  OnloanSum DOUBLE(32,6) DEFAULT 0,
  FinalBalance DOUBLE(32,6) DEFAULT 0,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_lastmonthsum (
  ID INTEGER(11) NOT NULL AUTO_INCREMENT,
  SubjectID VARCHAR(32) DEFAULT NULL,
  SubjectNo VARCHAR(60) DEFAULT NULL,
  SubjectName VARCHAR(200) DEFAULT NULL,
  YearMonth VARCHAR(16) DEFAULT NULL,
  BeginBalance DOUBLE(32,6) DEFAULT 0,
  Lead DOUBLE(32,6) DEFAULT 0,
  Onloan DOUBLE(32,6) DEFAULT 0,
  LeadSum DOUBLE(32,6) DEFAULT 0,
  OnloanSum DOUBLE(32,6) DEFAULT 0,
  FinalBalance DOUBLE(32,6) DEFAULT 0,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_subjectbudget (
  ID INTEGER(11) NOT NULL AUTO_INCREMENT,
  SubjectNo VARCHAR(60) DEFAULT NULL,
  BudgetYear VARCHAR(4) DEFAULT NULL,
  BudgetBalance DOUBLE(32,6) DEFAULT 0,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_voucher (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  VoucherNo VARCHAR(6) DEFAULT NULL,
  VoucherFrom VARCHAR(32) DEFAULT NULL,
  VoucherType VARCHAR(1) DEFAULT NULL,
  VoucherDate VARCHAR(16) DEFAULT NULL,
  IsAuditing VARCHAR(1) DEFAULT NULL,
  IsRecord VARCHAR(1) DEFAULT NULL,
  Director VARCHAR(50) DEFAULT NULL,
  Accountant VARCHAR(50) DEFAULT NULL,
  Assessor VARCHAR(50) DEFAULT NULL,
  DoBill VARCHAR(50) DEFAULT NULL,
  Addons TEXT COLLATE gbk_chinese_ci,
  AddonsCount VARCHAR(6) DEFAULT NULL,
  IsHasAlarm VARCHAR(1) DEFAULT NULL,
  DelFlag VARCHAR(1) DEFAULT NULL,
  ReverseVoucherID VARCHAR(32) DEFAULT NULL,
  SettleDate varchar(20) default NULL,
  SettleType varchar(32) default NULL,
  SettleNo varchar(100) default NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_entry (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  VoucherID VARCHAR(32) DEFAULT NULL,
  VSummary VARCHAR(200) DEFAULT NULL,
  SubjectNo VARCHAR(60) DEFAULT NULL,
  SubjectName VARCHAR(200) DEFAULT NULL,
  SumMoney DOUBLE(32,6) DEFAULT 0,
  CheckState varchar(20) default NULL,
  CheckDate varchar(20) default NULL,
  MatchBillD varchar(32) default NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_entrydata (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  EntryID VARCHAR(32) DEFAULT NULL,
  ESummary VARCHAR(200) DEFAULT NULL,
  Price DOUBLE(32,6) DEFAULT 0,
  Amount DOUBLE(32,6) DEFAULT 0,
  Balance DOUBLE(32,6) DEFAULT 0,
  PRIMARY KEY (ID)

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
  DesignID VARCHAR(32) DEFAULT NULL,
  ItemName TEXT COLLATE gbk_chinese_ci,
  RowNo TEXT COLLATE gbk_chinese_ci,
  ItemExpr0 TEXT COLLATE gbk_chinese_ci,
  ItemExpr1 TEXT COLLATE gbk_chinese_ci,
  InsertRows TEXT COLLATE gbk_chinese_ci,
  RowInfo TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_definereport (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  ReportName VARCHAR(200) DEFAULT NULL,
  ReportNote TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_definerowitem (
  ID VARCHAR(32) NOT NULL DEFAULT '',
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

CREATE TABLE cw_resclass (
  ID VARCHAR(160) NOT NULL DEFAULT '',
  ParentID VARCHAR(160) DEFAULT NULL,
  ClassName VARCHAR(100) DEFAULT NULL,
  LinkSubject VARCHAR(120) DEFAULT NULL,
  Measures VARCHAR(20) DEFAULT NULL,
  Notes TEXT COLLATE gbk_chinese_ci,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_rescard (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  CardNo VARCHAR(32) DEFAULT NULL,
  ResNo VARCHAR(100) DEFAULT NULL,
  ResName VARCHAR(200) DEFAULT NULL,
  ClassID VARCHAR(32) DEFAULT NULL,
  ClassName VARCHAR(200) DEFAULT NULL,
  ResUnit VARCHAR(20) DEFAULT NULL,
  ResAmount DOUBLE(32,6) DEFAULT 0,
  HasAmount DOUBLE(32,6) DEFAULT 0,
  ResPrice DOUBLE(32,6) DEFAULT 0,
  ResValue DOUBLE(32,6) DEFAULT 0,
  ResModel VARCHAR(100) DEFAULT NULL,
  DeptName VARCHAR(100) DEFAULT NULL,
  Locality VARCHAR(200) DEFAULT NULL,
  RelateFarmers DOUBLE(32,6) DEFAULT 0,
  UsedState VARCHAR(20) DEFAULT NULL,
  LiablePerson VARCHAR(50) DEFAULT NULL,
  LinkTel VARCHAR(20) DEFAULT NULL,
  StartLease VARCHAR(20) DEFAULT NULL,
  EndLease VARCHAR(20) DEFAULT NULL,
  ContractState TEXT COLLATE gbk_chinese_ci,
  BorderE VARCHAR(200) DEFAULT NULL,
  BorderW VARCHAR(200) DEFAULT NULL,
  BorderS VARCHAR(200) DEFAULT NULL,
  BorderN VARCHAR(200) DEFAULT NULL,
  ResUsage TEXT COLLATE gbk_chinese_ci,
  Notes TEXT COLLATE gbk_chinese_ci,
  Booker VARCHAR(20) DEFAULT NULL,
  BookType VARCHAR(20) DEFAULT NULL,
  BookTime DATETIME DEFAULT NULL,
  BookDate VARCHAR(20) DEFAULT NULL,
  ModifyDate VARCHAR(20) DEFAULT NULL,
  ResPicture VARCHAR(200) DEFAULT NULL,
  Manager VARCHAR(100) DEFAULT NULL,
  `name4` varchar(255) default NULL,
  `name5` varchar(255) default NULL,
  `name6` varchar(255) default NULL,
  `name7` varchar(255) default NULL,
  `name8` varchar(255) default NULL,
  `name9` varchar(255) default NULL,
  `name10` varchar(255) default NULL,
  `name11` varchar(255) default NULL,
  `name12` varchar(255) default NULL,
  `name13` varchar(255) default NULL,
  `name14` varchar(255) default NULL,
  `name15` varchar(255) default NULL,
  `name16` varchar(255) default NULL,
  `name17` varchar(255) default NULL,
  `name18` varchar(255) default NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_resleasecard (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  CardNo VARCHAR(32) DEFAULT NULL,
  CardType VARCHAR(20) DEFAULT NULL,
  LeaseHolder VARCHAR(50) DEFAULT NULL,
  LinkTel VARCHAR(20) DEFAULT NULL,
  ResourceID VARCHAR(32) DEFAULT NULL,
  ResourceName VARCHAR(200) DEFAULT NULL,
  LeaseType VARCHAR(20) DEFAULT NULL,
  IncomeSubject VARCHAR(100) DEFAULT NULL,
  PaySubject VARCHAR(50) DEFAULT NULL,
  ResUnit VARCHAR(20) DEFAULT NULL,
  ResAmount DOUBLE(32,6) DEFAULT 0,
  SumRental DOUBLE(32,6) DEFAULT 0,
  YearRental DOUBLE(32,6) DEFAULT 0,
  PayType VARCHAR(20) DEFAULT NULL,
  PayMoney DOUBLE(32,6) DEFAULT 0,
  NextPayDate VARCHAR(20) DEFAULT NULL,
  NextPayMoney DOUBLE(32,6) DEFAULT 0,
  StartLease VARCHAR(20) DEFAULT NULL,
  EndLease VARCHAR(20) DEFAULT NULL,
  BidType TEXT COLLATE gbk_chinese_ci,
  Appendix VARCHAR(200) DEFAULT NULL,
  Notes TEXT COLLATE gbk_chinese_ci,
  BookTime DATETIME DEFAULT NULL,
  BookDate VARCHAR(20) DEFAULT NULL,
  ModifyDate VARCHAR(20) DEFAULT NULL,
  LeaseState VARCHAR(20) DEFAULT NULL,
  ContractCo VARCHAR(200) DEFAULT NULL,
  ResUnitName VARCHAR(100) DEFAULT NULL,
  ContractNo VARCHAR(50) DEFAULT NULL,
  ContractDate VARCHAR(20) DEFAULT NULL,
  ContractName VARCHAR(200) DEFAULT NULL,
  ContractType VARCHAR(50) DEFAULT NULL,
  ContractMoney DOUBLE(32,6) DEFAULT 0,
  ContractYears DOUBLE(32,6) DEFAULT 0,
  ContractContent TEXT COLLATE gbk_chinese_ci,
  ContractNote TEXT COLLATE gbk_chinese_ci,
  DoState VARCHAR(20) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_respayperiod (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  CardID VARCHAR(32) DEFAULT NULL,
  PeriodName VARCHAR(100) DEFAULT NULL,
  PayMoney DOUBLE(32,6) DEFAULT 0,
  StartPay VARCHAR(20) DEFAULT NULL,
  EndPay VARCHAR(20) DEFAULT NULL,
  PayState VARCHAR(20) DEFAULT NULL,
  PayUser VARCHAR(20) DEFAULT NULL,
  PayDate VARCHAR(20) DEFAULT NULL,
  Notes TEXT COLLATE gbk_chinese_ci,
  VoucherID VARCHAR(32) DEFAULT NULL,
  DoState VARCHAR(20) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_flowlist (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  FlowType VARCHAR(20) DEFAULT NULL,
  FlowName VARCHAR(200) DEFAULT NULL,
  FlowCurrent VARCHAR(32) DEFAULT NULL,
  FlowStartDate VARCHAR(20) DEFAULT NULL,
  FlowState VARCHAR(20) DEFAULT NULL,
  FlowCreator VARCHAR(20) DEFAULT NULL,
  FlowCreateTime DATETIME DEFAULT NULL,
  Applicant VARCHAR(20) DEFAULT NULL,
  ApplyDate VARCHAR(20) DEFAULT NULL,
  AssessDate VARCHAR(20) DEFAULT NULL,
  ReplyDate VARCHAR(20) DEFAULT NULL,
  AuditState VARCHAR(20) DEFAULT NULL,
  Auditor VARCHAR(20) DEFAULT NULL,
  AuditOpinion MEDIUMTEXT,
  Appendices0 MEDIUMTEXT,
  Appendices1 MEDIUMTEXT,
  Appendices2 MEDIUMTEXT,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_flowmoney (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  FlowID VARCHAR(32) DEFAULT NULL,
  ApplyUsage VARCHAR(200) DEFAULT NULL,
  ApplyMoney DOUBLE(32,6) DEFAULT '0.000000',
  ApplyNotes VARCHAR(200) DEFAULT NULL,
  ReplyMoney DOUBLE(32,6) DEFAULT '0.000000',
  VoucherID VARCHAR(32) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_flowasset (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  FlowID VARCHAR(32) DEFAULT NULL,
  AssetCardID VARCHAR(32) DEFAULT NULL,
  AssetModel VARCHAR(100) DEFAULT NULL,
  LAmount DOUBLE(32,6) DEFAULT '0.000000',
  LYears DOUBLE(32,6) DEFAULT '0.000000',
  LTotalBalance DOUBLE(32,6) DEFAULT '0.000000',
  MyBasePrice DOUBLE(32,6) DEFAULT '0.000000',
  PayType VARCHAR(20) DEFAULT NULL,
  PayMoney DOUBLE(32,6) DEFAULT '0.000000',
  AssessBasePrice DOUBLE(32,6) DEFAULT NULL,
  ApplyNotes MEDIUMTEXT,
  ContractID VARCHAR(32) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_project (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  ProjectName VARCHAR(200) DEFAULT NULL,
  ProjectType VARCHAR(100) DEFAULT NULL,
  ProjectLocation VARCHAR(200) DEFAULT NULL,
  FundSource VARCHAR(200) DEFAULT NULL,
  ProjectBudget DOUBLE(36,6) DEFAULT '0.000000',
  ProjectLeader VARCHAR(100) DEFAULT NULL,
  ProjectSupervisor VARCHAR(100) DEFAULT NULL,
  ProjectIntroduction MEDIUMTEXT,
  AuditOpinion0 VARCHAR(200) DEFAULT NULL,
  AuditDate0 VARCHAR(20) DEFAULT NULL,
  AuditOpinion1 VARCHAR(200) DEFAULT NULL,
  AuditDate1 VARCHAR(20) DEFAULT NULL,
  RealFund DOUBLE(32,6) DEFAULT '0.000000',
  CompletionNotes MEDIUMTEXT,
  AuditOpinion2 VARCHAR(200) DEFAULT NULL,
  AuditDate2 VARCHAR(20) DEFAULT NULL,
  AuditOpinion3 VARCHAR(200) DEFAULT NULL,
  AuditDate3 VARCHAR(20) DEFAULT NULL,
  AuditOpinion4 VARCHAR(200) DEFAULT NULL,
  AuditDate4 VARCHAR(20) DEFAULT NULL,
  Appendices MEDIUMTEXT,
  CreateDate DATETIME DEFAULT NULL,
  Define1 VARCHAR(200) DEFAULT NULL,
  Define2 VARCHAR(200) DEFAULT NULL,
  Define3 VARCHAR(200) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_cashflow (
  ID VARCHAR(32) NOT NULL DEFAULT '',
  CashItemID VARCHAR(32) DEFAULT NULL,
  VoucherID VARCHAR(32) DEFAULT NULL,
  EntryID VARCHAR(32) DEFAULT NULL,
  PRIMARY KEY (ID)

)ENGINE=MyISAM;

CREATE TABLE cw_billcheck (
  ID varchar(32) NOT NULL,
  TownID varchar(32) default NULL,
  AccountID varchar(32) default NULL,
  BillBank varchar(200) default NULL,
  BillType varchar(20) default NULL,
  BillCurrency varchar(100) default NULL,
  BillNo varchar(200) default NULL,
  BillDate varchar(20) default NULL,
  BillPeriod double(15,3) default '0.000',
  BillUsage varchar(200) default NULL,
  SKDW varchar(100) default NULL,
  ReceiveMan varchar(50) default NULL,
  ReceiveDate varchar(20) default NULL,
  BillMoney double(15,3) default '0.000',
  ConsumeMan varchar(50) default NULL,
  ConsumeDate varchar(20) default NULL,
  ConsumeMoney double(15,3) default '0.000',
  Notes varchar(500) default NULL,
  UseState varchar(20) default NULL,
  BuyMan varchar(50) default NULL,
  VerifyMan varchar(50) default NULL,
  AttachFile varchar(200) default NULL,
  PRIMARY KEY  (ID)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

CREATE TABLE cw_billinvoice (
  ID varchar(32) NOT NULL,
  TownID varchar(32) default NULL,
  AccountID varchar(32) default NULL,
  BuyDate varchar(20) default NULL,
  OldTestDate varchar(20) default NULL,
  InvoiceType varchar(20) default NULL,
  InvoiceCode varchar(100) default NULL,
  InvoiceNo varchar(100) default NULL,
  InvoiceMoney double(15,3) default '0.000',
  BookMan varchar(50) default NULL,
  BookDate varchar(20) default NULL,
  CustomName varchar(100) default NULL,
  CustomTaxNo varchar(200) default NULL,
  DoBillMan varchar(50) default NULL,
  DoBillDate varchar(20) default NULL,
  DoBillMoney double(15,3) default '0.000',
  TaxRate double(15,3) default '0.000',
  TaxMoney double(15,3) default '0.000',
  SumMoney double(15,3) default '0.000',
  Notes varchar(200) default NULL,
  InvoiceState varchar(20) default NULL,
  OldTestState varchar(20) default NULL,
  CancelDate varchar(20) default NULL,
  SetRedDate varchar(20) default NULL,
  LostDate varchar(20) default NULL,
  LostNotes varchar(200) default NULL,
  IsRedInvoice varchar(20) default NULL,
  OldInvoiceID varchar(32) default NULL,
  DoBillSubject varchar(100) default NULL,
  AttachFile varchar(200) default NULL,
  PRIMARY KEY  (ID)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

CREATE TABLE cw_billreceipt (
  ID varchar(32) NOT NULL,
  TownID varchar(32) default NULL,
  AccountID varchar(32) default NULL,
  ReceiveDate varchar(20) default NULL,
  ReceiveNo varchar(200) default NULL,
  PayReason varchar(200) default NULL,
  PayUnit varchar(100) default NULL,
  PayMan varchar(50) default NULL,
  PayType varchar(32) default NULL,
  InvoiceNo varchar(100) default NULL,
  ReceiveMoney double(15,3) default '0.000',
  ReceiveMan varchar(50) default NULL,
  DoBillMan varchar(50) default NULL,
  ReveiveState varchar(20) default NULL,
  Notes varchar(200) default NULL,
  PRIMARY KEY  (ID)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

CREATE TABLE cw_billsettle (
  ID varchar(32) NOT NULL,
  SettleSubject varchar(100) default NULL,
  SettleDate varchar(20) default NULL,
  SettleType varchar(32) default NULL,
  SettleNo varchar(100) default NULL,
  SettleMoney double(15,3) default '0.000',
  CheckState varchar(20) default NULL,
  CheckDate varchar(20) default NULL,
  MatchEntryID varchar(32) default NULL,
  VoucherDate varchar(20) default NULL,
  VSummary varchar(200) default NULL,
  AttachFile varchar(200) default NULL,
  PRIMARY KEY  (ID)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

CREATE TABLE `cw_dayaccount` (
  `ID` varchar(32) NOT NULL,
  `AccSubjectNo` varchar(100) default NULL,
  `AccCurrency` varchar(20) default NULL,
  `VoucherDate` varchar(20) default NULL,
  `VoucherType` varchar(20) default NULL,
  `VoucherNo` varchar(20) default NULL,
  `EntryNo` varchar(20) default NULL,
  `DayNo` varchar(20) default NULL,
  `SettleType` varchar(20) default NULL,
  `SettleNo` varchar(20) default NULL,
  `SettleDate` varchar(20) default NULL,
  `AccMoney` double(32,6) default '0.000000',
  `LinkSubjectNo` varchar(100) default NULL,
  `Handler` varchar(50) default NULL,
  `DoBill` varchar(50) default NULL,
  `Notes` varchar(500) default NULL,
  `AttachFile` varchar(200) default NULL,
  `VoucherID` varchar(32) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

CREATE VIEW cw_viewsubjectsum AS 
  select 
    cw_subjectsum.ID AS ID,
    cw_subjectsum.SubjectID AS SubjectID,
    cw_subject.ParentNo AS ParentNo,
    cw_subject.SubjectNo AS SubjectNo,
    cw_subject.SubjectName AS SubjectName,
    cw_subject.SubjectType AS SubjectType,
    cw_subject.AccountType AS AccountType,
    cw_subject.IsDetail AS IsDetail,
    cw_subjectsum.YearMonth AS YearMonth,
    cw_subjectsum.BeginBalance AS BeginBalance,
    cw_subjectsum.Lead AS Lead,
    cw_subjectsum.Onloan AS Onloan,
    cw_subjectsum.LeadSum AS LeadSum,
    cw_subjectsum.OnloanSum AS OnloanSum,
    cw_subjectsum.FinalBalance AS FinalBalance 
  from 
    (cw_subject join cw_subjectsum on((cw_subject.ID = cw_subjectsum.SubjectID)));

CREATE VIEW cw_voucherentry AS 
  select 
    cw_entry.ID AS ID,
    cw_entry.VoucherID AS VoucherID,
    cw_entry.SubjectNo AS SubjectNo,
    cw_entry.SubjectName AS SubjectName,
    cw_entry.VSummary AS VSummary,
    cw_entry.SumMoney AS SumMoney,
	cw_voucher.VoucherNo AS VoucherNo,
    cw_voucher.VoucherType AS VoucherType,
    cw_voucher.VoucherDate AS VoucherDate,
    cw_voucher.IsAuditing AS IsAuditing,
    cw_voucher.IsRecord AS IsRecord,
    cw_voucher.Director AS Director,
    cw_voucher.Accountant AS Accountant,
    cw_voucher.Assessor AS Assessor,
    cw_voucher.DoBill AS DoBill,
    cw_voucher.AddonsCount AS AddonsCount,
    cw_voucher.DelFlag AS DelFlag,
    cw_entry.CheckState AS CheckState,
	cw_entry.CheckDate AS CheckDate,
	cw_entry.MatchBillD AS MatchBillD,
    cw_voucher.SettleDate AS SettleDate,
    cw_voucher.SettleType AS SettleType,
    cw_voucher.SettleNo AS SettleNo 
  from 
    (cw_entry join cw_voucher on((cw_entry.VoucherID = cw_voucher.ID)));

CREATE VIEW cw_voucherentrydata AS 
  select 
    cw_entrydata.ID AS ID,
    cw_entry.VoucherID AS VoucherID,
    cw_voucher.VoucherNo AS VoucherNo,
    cw_voucher.VoucherDate AS VoucherDate,
    cw_entrydata.EntryID AS EntryID,
    cw_entry.SubjectNo AS SubjectNo,
    cw_entry.SubjectName AS SubjectName,
    cw_entrydata.ESummary AS ESummary,
    cw_entrydata.Price AS Price,
    cw_entrydata.Amount AS Amount,
    cw_entrydata.Balance AS Balance,
    cw_entry.SumMoney AS SumMoney 
  from 
    ((cw_entry join cw_voucher on((cw_entry.VoucherID = cw_voucher.ID))) join cw_entrydata on((cw_entry.ID = cw_entrydata.EntryID)));

CREATE TABLE `cw_voucherdivide` (
  `ID` varchar(32) NOT NULL default '',
  `VoucherDate` varchar(16) default NULL,
  `VoucherSummary` varchar(200) default NULL,
  `AddonsCount` varchar(6) default NULL,
  `DivideSubjectNo` varchar(20) default NULL,
  `DivideType` varchar(20) default NULL,
  `DivideMoney` double(32,6) default '0.000000',
  `LinkSubject` varchar(20) default NULL,
  `IsCreateVoucher` varchar(20) default NULL,
  `VoucherID` varchar(32) default NULL,
  `DepositRate` double(32,6) default '0.000000',
  `QuickCalLess` double(32,6) default '0.000000',
  `Notes` varchar(500) default NULL,
  `DepositTerm` varchar(20) default NULL,
  `DefineYears` varchar(20) default NULL,
  `TaxRate` double(15,3) default '0.000',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

CREATE TABLE `sysinfo` (
  `ID` varchar(32) NOT NULL,
  `InfoType` varchar(32) default NULL,
  `InfoClass` varchar(32) default NULL,
  `InfoTitle` varchar(200) default NULL,
  `InfoAbstract` varchar(32) default NULL,
  `InfoContent` mediumtext,
  `ReadCounts` int(11) default '0',
  `PublicDate` varchar(20) default NULL,
  `PublicUnit` varchar(100) default NULL,
  `Publisher` varchar(50) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

CREATE TABLE `cw_matters` (
  `ID` varchar(32) NOT NULL,
  `MatterTitle` varchar(200) default NULL,
  `MatterType` varchar(20) default NULL,
  `txtDebit` varchar(100) default NULL,
  `txtCredit` varchar(100) default NULL,
  `txtSummary` varchar(200) default NULL,
  `txtMoney` double(32,6) default '0.000000',
  `VoucherID` varchar(100) default NULL,
  `IsCreateVoucher` varchar(20) default NULL,
  `MatterContent` mediumtext,
  `FlowStep` varchar(20) default NULL,
  `FlowNote1` mediumtext,
  `FlowNote2` mediumtext,
  `FlowNote3` mediumtext,
  `FlowNote4` mediumtext,
  `FlowNote5` mediumtext,
  `FlowNote6` mediumtext,
  `FlowNote7` mediumtext,
  `FlowNote8` mediumtext,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;

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

CREATE TABLE `contidion` (
  `ID` int(11) NOT NULL auto_increment,
  `cname` varchar(200) default NULL,
  `con` varchar(500) default NULL,
  `Step1` double default '0',
  `Step2` double default '0',
  `Bili` double default '0',
  `BiliShow` varchar(500) default NULL,
  `UserTitles` text,
  `DeptID` int(11) default '0',
  `DelFlag` tinyint(4) default '0',
  `SubTime` int(11) default '0',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `contidiontotitle` (
  `ID` int(11) NOT NULL auto_increment,
  `define1` varchar(500) default NULL,
  `define2` varchar(500) default NULL,
  `define3` varchar(500) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `ctdjb` (
  `ID` int(11) NOT NULL auto_increment,
  `xmID` int(11) default NULL,
  `xmmc` text,
  `subTime` varchar(255) default NULL,
  `xh` int(11) default NULL,
  `tbr` text,
  `zizhi` text,
  `fzr` text,
  `lxdh` text,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `cunwugongkai` (
  `ID` int(11) NOT NULL auto_increment,
  `Title` varchar(100) default NULL COMMENT '标题',
  `DeptID` int(11) default '0' COMMENT '部门ID',
  `DeptName` varchar(100) default NULL,
  `FileName` varchar(200) default NULL COMMENT '文件名称',
  `SubTime` varchar(50) default NULL,
  `LastUpdate` varchar(50) default NULL,
  `SubUID` int(11) default '0',
  `DelFlag` tinyint(4) default '0',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `cyss` (
  `ID` int(11) NOT NULL auto_increment,
  `Title` text,
  `xmmc` text,
  `xmsssj` text,
  `xmmx` text,
  `danwei` text,
  `subTime` varchar(50) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `daili` (
  `DaiLi_ID` int(11) NOT NULL auto_increment,
  `DeptID` int(11) default '0' COMMENT '部门ID',
  `VillageName` char(50) default NULL COMMENT '申请村',
  `ApplyDate` char(50) default NULL COMMENT '申请日期',
  `ProjectName` char(100) default NULL COMMENT '申请项目',
  `ProjectType` char(10) default '0' COMMENT '项目类型  0：收入项目 1：支出项目',
  `EstimateValue` char(50) default NULL COMMENT '估算价',
  `ZhiBu_YuanYin` char(100) default NULL,
  `ZhiBu_Time` int(11) default NULL,
  `ZhiBu_ShiXiang` char(100) default NULL,
  `LiangWei_Time` int(11) default NULL COMMENT '两委决定时间',
  `ShenYi_Tiime` int(11) default NULL,
  `YiAnGongKai_FromDate` int(11) default NULL COMMENT '议案公开开始时间',
  `YiAnGongKai_EndDate` int(11) default NULL COMMENT '议案公开结束时间',
  `CunMin_Time` int(11) default NULL,
  `CunMin_TotalNum` int(11) default NULL,
  `CunMin_AttendNum` int(11) default NULL,
  `CunMin_PassNum` int(11) default NULL,
  `CunMin_PassRate` double default NULL,
  `JieGuoGongKai_FromDate` int(11) default NULL,
  `JieGuoGongKai_EndDate` int(11) default NULL,
  `ZhiBuShuJi` char(50) default NULL,
  `CuZhuren` char(50) default NULL,
  `CunWuJianDuZuZhang` char(50) default NULL,
  `LianZhengJianDuYuan` char(50) default NULL,
  `XiangZhenYiJian` char(50) default NULL,
  `SubTime` char(50) default NULL COMMENT '提交时间',
  `SubUID` int(11) default NULL COMMENT '提交用户UID',
  `DelFlag` tinyint(4) default '0' COMMENT '删除标记',
  `time1` char(50) default NULL,
  `time2` char(50) default NULL,
  `time3` char(50) default NULL,
  `time4` char(50) default NULL,
  `time5` char(50) default NULL,
  `time6` char(50) default NULL,
  `time7` char(50) default NULL,
  `time8` char(50) default NULL,
  `time9` char(50) default NULL,
  `value1` char(50) default NULL,
  `value2` char(50) default NULL,
  `value3` char(50) default NULL,
  `value4` char(50) default NULL,
  `value5` char(50) default NULL,
  `value6` char(100) default NULL,
  PRIMARY KEY  (`DaiLi_ID`)
) ENGINE=MyISAM;

CREATE TABLE `dailibarcode` (
  `ID` int(11) NOT NULL auto_increment,
  `DaiLi_ID` int(11) default NULL,
  `UID` int(11) default '0',
  `BarCode` char(50) default NULL,
  `SubTime` char(50) default '0' COMMENT '提交时间',
  `DelFlag` tinyint(4) default '0',
  `time1` char(100) default NULL,
  `time2` char(100) default NULL,
  `time3` char(100) default NULL,
  `time4` char(100) default NULL,
  `time5` char(100) default NULL,
  `time6` char(100) default NULL,
  `time7` char(100) default NULL,
  `time8` char(100) default NULL,
  `time9` char(100) default NULL,
  `value1` char(100) default NULL,
  `value2` char(100) default NULL,
  `value3` char(100) default NULL,
  `value4` char(100) default NULL,
  `value5` char(100) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `department` (
  `id` int(11) NOT NULL auto_increment,
  `parentid` int(11) default '0' COMMENT '父ID',
  `DeptName` char(50) default NULL COMMENT '部门名称',
  `SubUID` int(11) default '0' COMMENT '提交者UID',
  `SubTime` int(11) default NULL COMMENT ' 提交时间',
  `DelFlag` tinyint(4) default '0' COMMENT '删除标记',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM;

CREATE TABLE `jjztb` (
  `ID` int(11) NOT NULL auto_increment,
  `xmmc` text,
  `subTime` varchar(50) default NULL,
  `adress` text,
  `cyry` text,
  `zcr` text,
  `cbr` text,
  `jlr` text,
  `zynr` text,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `mzcp` (
  `ID` int(11) NOT NULL auto_increment,
  `Title` text,
  `Content` text,
  `DanWei` text,
  `AddTime` varchar(50) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `pibanka` (
  `PID` int(11) NOT NULL auto_increment,
  `DeptID` int(11) default '0' COMMENT '单位ID',
  `OutReason` mediumtext,
  `OutMoney` double default '0' COMMENT '出支金额',
  `State` tinyint(4) default '0' COMMENT '批审状态 0：未通过 1：通过',
  `SubUID` int(11) default NULL COMMENT '提交用户ID',
  `SubTime` char(50) default NULL COMMENT '交提时间',
  `DelFlag` tinyint(4) default '0' COMMENT '除删标记',
  `d` varchar(5000) default NULL,
  `c` char(50) default NULL,
  `lujing` varchar(5000) default NULL,
  `zhaiyao` char(50) default NULL,
  `subid` char(50) default NULL,
  `time1` char(100) default NULL,
  `time2` char(100) default NULL,
  `time3` char(100) default NULL,
  `time4` char(100) default NULL,
  `time5` char(100) default NULL,
  `time6` char(100) default NULL,
  `value1` char(100) default NULL,
  `value2` char(100) default NULL,
  `value3` char(100) default NULL,
  `time7` char(100) default NULL,
  `time8` char(100) default NULL,
  `time9` char(100) default NULL,
  `value4` char(100) default NULL,
  `value5` char(100) default NULL,
  `value6` char(100) default NULL,
  `value7` char(100) default NULL,
  `value8` char(100) default NULL,
  `value9` char(100) default NULL,
  PRIMARY KEY  (`PID`)
) ENGINE=MyISAM;

CREATE TABLE `pibankabarcode` (
  `ID` int(11) NOT NULL auto_increment,
  `PID` int(11) default '0' COMMENT '批办卡ID',
  `UID` int(11) default '0' COMMENT '用户UID',
  `BarCode` char(50) default NULL,
  `SubTime` char(50) default NULL COMMENT '提交时间',
  `DelFlag` tinyint(4) default '0' COMMENT '删除标记',
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `project` (
  `ID` int(11) NOT NULL auto_increment,
  `ProjectName` char(100) default NULL COMMENT '项目名称',
  `SubTime` int(11) default '0' COMMENT '交提时间',
  `SubUID` int(11) default '0' COMMENT '提交用户ID',
  `DelFlag` tinyint(4) default '0',
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
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

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

CREATE TABLE `xiangmu` (
  `ID` int(11) NOT NULL auto_increment,
  `xmmc` varchar(100) default NULL,
  `DelFlag` tinyint(4) default '0' COMMENT '删除标记',
  `dailiid` char(50) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `zbgg` (
  `ID` int(11) NOT NULL auto_increment,
  `cwh` text,
  `zbgc` text,
  `nrqk` text,
  `bmtj` text,
  `yz` text,
  `startTime` varchar(50) default NULL,
  `finishTime` varchar(50) default NULL,
  `bmdd` text,
  `lxfs` text,
  `subTime` varchar(50) default NULL,
  `str` varchar(200) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `zhongb` (
  `ID` int(11) NOT NULL auto_increment,
  `ncmc` text,
  `startTime` varchar(50) default NULL,
  `finishTime` varchar(50) default NULL,
  `zbmc` text,
  `dwa` text,
  `dwb` text,
  `dwc` text,
  `dws` int(11) default NULL,
  `zbdw` text,
  `ztbdw` text,
  `subTime` varchar(50) default NULL,
  `str` varchar(200) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM;

CREATE TABLE `cw_barcode` (
  `ID` varchar(100) NOT NULL,
  `UserID` varchar(50) default NULL,
  `barcode` varchar(50) default NULL,
  `usedate` varchar(50) default NULL,
  `usestate` varchar(20) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=gbk;