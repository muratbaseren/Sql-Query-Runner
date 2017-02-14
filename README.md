#SQL Query Runner With Reporter & Notify

Simple SQL Query Runner. Set options in app.config file and getting notify or mail report.

You can set AppSettings and ConnectionString tags into app.config file. Application runs your sql query at your specifying time. After that it reports as notify popup window or mail.

----------


> &lt;**connectionStrings**&gt;
> 		
> 
> > &lt;add name="**DefaultConnection**" connectionString="Server=[**ServerName**];Database=**Northwind**;Trusted_Connection=**true**;"/&gt;
> 
> &lt;/**connectionStrings**&gt;


----------

 

> &lt;**appSettings**&gt;
> 
> > &lt;add key="**appTitle**" value="SQL Query Runner"/&gt; 	
> > &lt;add key="**query**" value="SELECT * FROM Customers"/&gt; 	
> > &lt;add key="**interval**" value="5000" /&gt; 	
> > &lt;add key="**labelTextPrefix**" value="Row(s) affected :"/&gt; 	
> > &lt;add key="**visible**" value="true"/&gt; 	
> > &lt;add key="**resultType**" value="table"/&gt; &lt;!--"**table**" : for select query or "**count**" : for
> > insert, update, delete vs--&gt;
> > 
> > &lt;!--***Notification Options***--&gt; 	
> > &lt;add key="**showNotification**" value="true"/&gt; 
> > &lt;add key="**notifyTipTitle**" value="SQL Query Runner" /&gt; 	
> > &lt;add key="**notifyTipText**" value="Row(s) affected : {0}" /&gt;
> > 
> > &lt;!--**Mail Options**--&gt; 	
> > &lt;add key="**sendMail**" value="false"/&gt;
> > &lt;add key="**mailServer**" value="smtp.gmail.com"/&gt; 	
> > &lt;add key="**mailPort**" value="587"/&gt; 	
> > &lt;add key="**mailFrom**" value="mail@gmail.com"/&gt; 	
> > &lt;add key="**mailPassword**" value="P@ssw0rd"/&gt; 	
> > &lt;add key="**mailTo**" value="receiver@gmail.com"/&gt; 	
> > &lt;add key="**subject**" value="SQL Query Runner Notification"/&gt;  
> > &lt;add key="**mailBody**" value="SQL Query Runner ran a query. Query result is below;{br}{br}Date-Time : {datetime}{br}Row(s)
> > affected : {count}{br}{br}{hr}{table}" /&gt;
> 
> &lt;/**appSettings**&gt;
