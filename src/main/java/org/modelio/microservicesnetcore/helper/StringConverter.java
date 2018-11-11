package org.modelio.microservicesnetcore.helper;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class StringConverter {
		
	public static String CamelCaseToSnakeCase(String text)
	{
		Matcher m = Pattern.compile("(?<=[a-z])[A-Z]").matcher(text);

		StringBuffer sb = new StringBuffer();
		while (m.find()) {
		    m.appendReplacement(sb, "_"+m.group().toLowerCase());
		}
		m.appendTail(sb);
		
		return sb.toString().toLowerCase();
	}
	
	public static String SnakeCaseToCamelCase(String text)
	{
		String[] m = text.split("_");
		String result="";
		for (String tmp : m ) {
			result+=tmp.substring(0, 1).toUpperCase() + tmp.substring(1);
		}
		
		return result;
	}
}
