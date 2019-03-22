package org.modelio.microservicesnetcore.code.generator;

import java.io.File;
import java.io.FileWriter;

import org.modelio.metamodel.uml.statik.Attribute;
import org.modelio.metamodel.uml.statik.Classifier;
import org.modelio.metamodel.uml.statik.Package;

import modeliotools.treevisitor.HandlerAdapter;

public class GenerateIRepoProjectCodeHandler extends HandlerAdapter {
	private String _path;
	private IRepositoryProjectTemplate _template;
	
	public GenerateIRepoProjectCodeHandler(String applicationName,Package domain,String path)
	{
		_path=path+"/irepository";
		_template=new IRepositoryProjectTemplate(applicationName, domain);
		
		// créer le répertoire project si il n'existe pas
		File fileDir = new File(path);
		fileDir.mkdirs();
		
		// créer le csproj
		String name=applicationName+"."+domain.getName()+".irepository.csproj";
		
		StringBuffer content = new StringBuffer("");
		content.append(_template.getCsProj());
		if(content.length()>0)
		{
			try {
				File csprojFile =new File(_path+"/"+name);
				csprojFile.createNewFile();
				FileWriter writer = new FileWriter(csprojFile);
				try {
	                writer.write(content.toString());
	            } finally {
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        } catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}
	}
	
	@Override
	protected void beginVisitingClassifier(Classifier visited) 
	{
		String name=visited.getName()+".cs";
		
		StringBuffer content = new StringBuffer("");
		content.append(_template.getHeader(visited));
		for (Attribute attr : visited.getOwnedAttribute()) {
			if (!attr.getName().equals("id"))
				content.append(_template.getAttribute(attr));
		}
		content.append(_template.getEnd());
		if(content.length()>0)
		{
			try {
				File csFile =new File(_path+"/"+name);
				csFile.createNewFile();
				FileWriter writer = new FileWriter(csFile);
				try 
				{
	                writer.write(content.toString());
	            } 
				finally 
				{
	                // quoiqu'il arrive, on ferme le fichier
	                writer.close();
	            }
	        }
			catch (Exception e) {
	            System.out.println("Impossible de creer le fichier : "+_path+"/"+name);
	        }
		}
	}
}
