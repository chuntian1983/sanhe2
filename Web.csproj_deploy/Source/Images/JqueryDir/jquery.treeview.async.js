/*
 * Async Treeview 0.1 - Lazy-loading extension for Treeview
 * 
 * http://bassistance.de/jquery-plugins/jquery-plugin-treeview/
 *
 * Copyright (c) 2007 Jörn Zaefferer
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 *
 * Revision: $Id$
 *
 */

;(function($) {

function load(settings, root, child, container) {
	$.getJSON(settings.url, {root: root}, function(response) {
		var cicos=settings.cicos;
		var selectNodes=settings.selectNodes?settings.selectNodes:$("#SelectNodes");
		function createNode(parent) {
			if (!settings.showcheck)
			{
			    var current = $("<li/>").attr("id", this.id || "").html("<span>" + this.text + "</span>").appendTo(parent);
			} else {
			    var myico=selectNodes.val().indexOf("c"+this.id)==-1?cicos[0]:cicos[1];
			    var current = $("<li/>").attr("id", this.id || "").html("<span><img name=commCheckBox id=c" + this.id + " pid=c" + this.pid + " src=" + myico + ">" + this.text + "</span>").appendTo(parent);
			    $("#c"+this.id).click(function(e) {
		            var ischeck = (this.src.indexOf(cicos[0])!=-1||this.src.indexOf(cicos[2])!=-1);
		            setCheckState(this,ischeck)
	                cascade(this.id,ischeck);
	                bubble(this.pid,document.getElementsByName("commCheckBox"));
			    });
			}
			if (this.classes) {
				current.children("span").addClass(this.classes);
			}
			if (this.expanded) {
				current.addClass("open");
			}
			if (this.hasChildren || this.children && this.children.length) {
				var branch = $("<ul/>").appendTo(current);
				if (this.hasChildren) {
					current.addClass("hasChildren");
					createNode.call({
						text:"placeholder",
						id:"placeholder",
						children:[]
					}, branch);
				}
				if (this.children && this.children.length) {
					$.each(this.children, createNode, [branch])
				}
			}
		}
        //遍历子节点
        function cascade(mid, ischeck) {
            var nodes=document.getElementsByName("commCheckBox");
            for(var i=0;i<nodes.length;i++)
            {
                if(nodes[i].pid==mid)
                {
                    setCheckState(nodes[i],ischeck)
                    cascade(nodes[i].id, ischeck);
                }
            }
        }
        //上溯父节点
        function bubble(pid, nodes) {
            var p=document.getElementById(pid);
            if(p)
            {
                var ischeck=false;
                for(var i=0;i<nodes.length;i++)
                {
                    if(nodes[i].pid==pid&&nodes[i].src.indexOf(cicos[1])!=-1)
                    {
                        ischeck=true;
                    }
                }
                setCheckState(p,ischeck);
                bubble(p.pid, nodes);
            }
        }
        function setCheckState(n,ischeck) {
            n.src=ischeck?cicos[1]:cicos[0];
            selectNodes.val(selectNodes.val().replace(n.id,""));
            if(ischeck)
            {
                selectNodes.val(selectNodes.val()+n.id);
            }
        }
		$.each(response, createNode, [child]);
        $(container).treeview({add: child});
        CloseMaskLay();
    });
}

var proxied = $.fn.treeview;
$.fn.treeview = function(settings) {
	if (!settings.url) {
		return proxied.apply(this, arguments);
	}
	if(settings.cicos) {
	    var cicos=settings.cicos;
	    for(var i=0; i<cicos.length; i++)
	    {
	        var im = new Image();
	        im.src = cicos[i];
	    }
	}
	var container = this;
	load(settings, "source", this, container);
	var userToggle = settings.toggle;
	return proxied.call(this, $.extend({}, settings, {
		collapsed: true,
		toggle: function() {
			var $this = $(this);
			if ($this.hasClass("hasChildren")) {
				ShowMaskLay();
				var childList = $this.removeClass("hasChildren").find("ul");
				childList.empty();
				load(settings, this.id, childList, container);
			}
			if (userToggle) {
				userToggle.apply(this, arguments);
			}
		}
	}));
};

})(jQuery);