TYPE=VIEW
query=select `cwdb100079`.`cw_entrydata`.`ID` AS `ID`,`cwdb100079`.`cw_entry`.`VoucherID` AS `VoucherID`,`cwdb100079`.`cw_voucher`.`VoucherNo` AS `VoucherNo`,`cwdb100079`.`cw_voucher`.`VoucherDate` AS `VoucherDate`,`cwdb100079`.`cw_entrydata`.`EntryID` AS `EntryID`,`cwdb100079`.`cw_entry`.`SubjectNo` AS `SubjectNo`,`cwdb100079`.`cw_entry`.`SubjectName` AS `SubjectName`,`cwdb100079`.`cw_entrydata`.`ESummary` AS `ESummary`,`cwdb100079`.`cw_entrydata`.`Price` AS `Price`,`cwdb100079`.`cw_entrydata`.`Amount` AS `Amount`,`cwdb100079`.`cw_entrydata`.`Balance` AS `Balance`,`cwdb100079`.`cw_entry`.`SumMoney` AS `SumMoney` from ((`cwdb100079`.`cw_entry` join `cwdb100079`.`cw_voucher` on((`cwdb100079`.`cw_entry`.`VoucherID` = `cwdb100079`.`cw_voucher`.`ID`))) join `cwdb100079`.`cw_entrydata` on((`cwdb100079`.`cw_entry`.`ID` = `cwdb100079`.`cw_entrydata`.`EntryID`)))
md5=0ff27958bd162be88909c91a9744e1ec
updatable=1
algorithm=0
definer_user=root
definer_host=localhost
suid=1
with_check_option=0
revision=1
timestamp=2014-12-19 13:12:39
create-version=1
source=select \n    cw_entrydata.ID AS ID,\n    cw_entry.VoucherID AS VoucherID,\n    cw_voucher.VoucherNo AS VoucherNo,\n    cw_voucher.VoucherDate AS VoucherDate,\n    cw_entrydata.EntryID AS EntryID,\n    cw_entry.SubjectNo AS SubjectNo,\n    cw_entry.SubjectName AS SubjectName,\n    cw_entrydata.ESummary AS ESummary,\n    cw_entrydata.Price AS Price,\n    cw_entrydata.Amount AS Amount,\n    cw_entrydata.Balance AS Balance,\n    cw_entry.SumMoney AS SumMoney \n  from \n    ((cw_entry join cw_voucher on((cw_entry.VoucherID = cw_voucher.ID))) join cw_entrydata on((cw_entry.ID = cw_entrydata.EntryID)))
