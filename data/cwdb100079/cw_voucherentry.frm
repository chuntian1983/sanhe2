TYPE=VIEW
query=select `cwdb100079`.`cw_entry`.`ID` AS `ID`,`cwdb100079`.`cw_entry`.`VoucherID` AS `VoucherID`,`cwdb100079`.`cw_entry`.`SubjectNo` AS `SubjectNo`,`cwdb100079`.`cw_entry`.`SubjectName` AS `SubjectName`,`cwdb100079`.`cw_entry`.`VSummary` AS `VSummary`,`cwdb100079`.`cw_entry`.`SumMoney` AS `SumMoney`,`cwdb100079`.`cw_voucher`.`VoucherNo` AS `VoucherNo`,`cwdb100079`.`cw_voucher`.`VoucherType` AS `VoucherType`,`cwdb100079`.`cw_voucher`.`VoucherDate` AS `VoucherDate`,`cwdb100079`.`cw_voucher`.`IsAuditing` AS `IsAuditing`,`cwdb100079`.`cw_voucher`.`IsRecord` AS `IsRecord`,`cwdb100079`.`cw_voucher`.`Director` AS `Director`,`cwdb100079`.`cw_voucher`.`Accountant` AS `Accountant`,`cwdb100079`.`cw_voucher`.`Assessor` AS `Assessor`,`cwdb100079`.`cw_voucher`.`DoBill` AS `DoBill`,`cwdb100079`.`cw_voucher`.`AddonsCount` AS `AddonsCount`,`cwdb100079`.`cw_voucher`.`DelFlag` AS `DelFlag`,`cwdb100079`.`cw_entry`.`CheckState` AS `CheckState`,`cwdb100079`.`cw_entry`.`CheckDate` AS `CheckDate`,`cwdb100079`.`cw_entry`.`MatchBillD` AS `MatchBillD`,`cwdb100079`.`cw_voucher`.`SettleDate` AS `SettleDate`,`cwdb100079`.`cw_voucher`.`SettleType` AS `SettleType`,`cwdb100079`.`cw_voucher`.`SettleNo` AS `SettleNo` from (`cwdb100079`.`cw_entry` join `cwdb100079`.`cw_voucher` on((`cwdb100079`.`cw_entry`.`VoucherID` = `cwdb100079`.`cw_voucher`.`ID`)))
md5=a8ac4a9400f6ae29f7f5ae5fb01532c0
updatable=1
algorithm=0
definer_user=root
definer_host=localhost
suid=1
with_check_option=0
revision=1
timestamp=2014-12-16 16:12:50
create-version=1
source=select \n    cw_entry.ID AS ID,\n    cw_entry.VoucherID AS VoucherID,\n    cw_entry.SubjectNo AS SubjectNo,\n    cw_entry.SubjectName AS SubjectName,\n    cw_entry.VSummary AS VSummary,\n    cw_entry.SumMoney AS SumMoney,\n    cw_voucher.VoucherNo AS VoucherNo,\n    cw_voucher.VoucherType AS VoucherType,\n    cw_voucher.VoucherDate AS VoucherDate,\n    cw_voucher.IsAuditing AS IsAuditing,\n    cw_voucher.IsRecord AS IsRecord,\n    cw_voucher.Director AS Director,\n    cw_voucher.Accountant AS Accountant,\n    cw_voucher.Assessor AS Assessor,\n    cw_voucher.DoBill AS DoBill,\n    cw_voucher.AddonsCount AS AddonsCount,\n    cw_voucher.DelFlag AS DelFlag,\n    cw_entry.CheckState AS CheckState,\n    cw_entry.CheckDate AS CheckDate,\n    cw_entry.MatchBillD AS MatchBillD,\n    cw_voucher.SettleDate AS SettleDate,\n    cw_voucher.SettleType AS SettleType,\n    cw_voucher.SettleNo AS SettleNo \n  from \n    (cw_entry join cw_voucher on((cw_entry.VoucherID = cw_voucher.ID)))
