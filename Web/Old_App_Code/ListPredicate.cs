using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// List<T>搜索类
/// </summary>
public class ListPredicate
{
    private string matchValue = string.Empty;
    private List<string> list = new List<string>();

    public ListPredicate()
	{
		//--
	}

    public void AddListItem(string _value)
    {
        if (list.Contains(_value) == false)
        {
            list.Add(_value);
        }
    }

    public bool CheckStartsWith(string _value)
    {
        matchValue = _value;
        return list.Exists(new Predicate<string>(MatchStartsWith));
    }

    public bool CheckStartsWith(string _value, bool isAdd)
    {
        bool isExist = CheckStartsWith(_value);
        if (isAdd && isExist == false)
        {
            AddListItem(_value);
        }
        return isExist;
    }

    private bool MatchStartsWith(string _item)
    {
        return matchValue.StartsWith(_item);
    }
}
