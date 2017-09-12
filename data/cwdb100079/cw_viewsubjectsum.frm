TYPE=VIEW
query=select `cwdb100079`.`cw_subjectsum`.`ID` AS `ID`,`cwdb100079`.`cw_subjectsum`.`SubjectID` AS `SubjectID`,`cwdb100079`.`cw_subject`.`ParentNo` AS `ParentNo`,`cwdb100079`.`cw_subject`.`SubjectNo` AS `SubjectNo`,`cwdb100079`.`cw_subject`.`SubjectName` AS `SubjectName`,`cwdb100079`.`cw_subject`.`SubjectType` AS `SubjectType`,`cwdb100079`.`cw_subject`.`AccountType` AS `AccountType`,`cwdb100079`.`cw_subject`.`IsDetail` AS `IsDetail`,`cwdb100079`.`cw_subjectsum`.`YearMonth` AS `YearMonth`,`cwdb100079`.`cw_subjectsum`.`BeginBalance` AS `BeginBalance`,`cwdb100079`.`cw_subjectsum`.`Lead` AS `Lead`,`cwdb100079`.`cw_subjectsum`.`Onloan` AS `Onloan`,`cwdb100079`.`cw_subjectsum`.`LeadSum` AS `LeadSum`,`cwdb100079`.`cw_subjectsum`.`OnloanSum` AS `OnloanSum`,`cwdb100079`.`cw_subjectsum`.`FinalBalance` AS `FinalBalance` from (`cwdb100079`.`cw_subject` join `cwdb100079`.`cw_subjectsum` on((`cwdb100079`.`cw_subject`.`ID` = `cwdb100079`.`cw_subjectsum`.`SubjectID`)))
md5=ee05fd51e04c129f18c458c160bc1935
updatable=1
algorithm=0
definer_user=root
definer_host=localhost
suid=1
with_check_option=0
revision=1
timestamp=2014-12-16 16:12:49
create-version=1
source=select \n    cw_subjectsum.ID AS ID,\n    cw_subjectsum.SubjectID AS SubjectID,\n    cw_subject.ParentNo AS ParentNo,\n    cw_subject.SubjectNo AS SubjectNo,\n    cw_subject.SubjectName AS SubjectName,\n    cw_subject.SubjectType AS SubjectType,\n    cw_subject.AccountType AS AccountType,\n    cw_subject.IsDetail AS IsDetail,\n    cw_subjectsum.YearMonth AS YearMonth,\n    cw_subjectsum.BeginBalance AS BeginBalance,\n    cw_subjectsum.Lead AS Lead,\n    cw_subjectsum.Onloan AS Onloan,\n    cw_subjectsum.LeadSum AS LeadSum,\n    cw_subjectsum.OnloanSum AS OnloanSum,\n    cw_subjectsum.FinalBalance AS FinalBalance \n  from \n    (cw_subject join cw_subjectsum on((cw_subject.ID = cw_subjectsum.SubjectID)))
