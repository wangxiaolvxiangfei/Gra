$(function(){
	$(".ly-ul-parent li").hover(function(){
		$(".ly-ul").stop(true,true);
		$(this).find($(".ly-ul")).show();
	},function(){
		$(".ly-ul").stop(true,true);
		$(this).find($(".ly-ul")).hide();
	})

	$(".ly-ul li").hover(function(){
		$(".ly-ul").stop(true,true);
		$(this).find($(".ly-ul-child")).show();

	},function(){
		$(".ly-ul").stop(true,true);
		$(this).find($(".ly-ul-child")).hide();
	})


	//是否曾转专业
	$('.changeFaculty').find("input[type=radio]").click(function(){
		var index=$(this).index();
		if(index==1){
			$('.changeFaculty').find("input[type=text]").attr("readonly",false);
		}else{
			$('.changeFaculty').find("input[type=text]").attr("readonly",true);
		}
	})


	//选择
	$(".index").click(function(){
		var parent=$(this).parents("tr");
		var value=parent.find("td:eq(3)").text();
		$(".indexInput").val(value);
	})
	$(".departmentName").click(function(){
		var parent=$(this).parents("tr");
		var value1=parent.find("td:eq(3)").text();
		$(".departmentNameInput1").val(value1);
		var value2=parent.find("td:eq(4)").text();
		$(".departmentNameInput2").val(value2);

	})
	$(".professionName").click(function(){
		var parent=$(this).parents("tr");
		var value1=parent.find("td:eq(4)").text();
		$(".professionNameInput1").val(value1);
		var value2=parent.find("td:eq(3)").text();
		$(".professionNameInput2").val(value2);

	})
	$(".loginTimeControl").click(function(){
		var parent=$(this).parents("tr");
		var value1=parent.find("td:eq(1)").text();
		$(".loginTimeControlInput1").val(value1);
		var value2=parent.find("td:eq(2)").text();
		$(".loginTimeControlInput2").val(value2);
	})
	$(".schoolChange").click(function(){
		var parent=$(this).parents("tr");
		var value1=parent.find("td:eq(2)").text();
		$(".schoolChangeInput1").val(value1);
		var value2=parent.find("td:eq(3)").text();
		$(".schoolChangeInput2").val(value2);
	})
	//全选
	var select=false
	$(".checkboxSelect").click(function(){
		var parent=$(this).parents("table");
		var tr=parent.find("tr")
		var length=parent.find("tr").length;
		if(!select){
			for(var i=0;i<length;i++){
				$(tr[i]).find("td:eq(0)").find("input").attr("checked",true)
			}
			select=true;
		}else{
			for(var i=0;i<length;i++){
				$(tr[i]).find("td:eq(0)").find("input").attr("checked",false)
			}
			select=false;
		}
		
	})
})