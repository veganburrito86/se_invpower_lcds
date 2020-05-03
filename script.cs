/*
 *  -----------
 *  VB86_Inventory_LCDs v1.4
 *  by VeganBurrito86
 *  
 *  Script to count all inventory and display ore lists, ingot lists, volume and mass information, and various power information.
 * 
 *  In this update, many optimizations were made. Most notably, newly-placed blocks do not get updated as quickly as they did before. Panels, containers, and power producers are automatically searched for every 120 seconds, but each of these three calls happen in offset from each other. This was done to drastrically save on the number of calls (and the number of simultaneous calls) to search through all blocks on your grids, which can easily cause lag. If you do not see newly-placed blocks right away, you could either simply hit "Recompile" on the programming block to update everything, or, see the other options at the bottom of this readme to update all blocks or certain types of blocks.
 *  
 *  [ PANELS ]
 *  Name your LCD panels "invPanel XXXXXX", replace "XXXXXX" with one of the following options:
 *  
 *  percent            - simply displays the percentage of inventory used up
 *  ore                - displays a list of all ore in grid as well as % of inventory used at top
 *  ingots             - displays a list of all ingots in grid as well as % of inventory used at top
 *  orelist            - displays only a list of ore
 *  ingotslist         - displays only a list of ingots
 *  mass               - displays the total mass of the grid
 *  power              - displays total output, stored power remaining, # of batteries, total battery input & output, and % of output distribution for each type of power source
 *  storedpower        - simply displays the percentage of stored power remaining
 *  [NEW] batteries    - displays the remaining amount of time until your batteries are depleted
 *  [NEW] powerreq     - (must use power_assessment argument) displays total estimated power needed for entire grid and a list of all powered blocks and how much power they take running at max
 *  [NEW] powerneeds   - (must use power_assessment argument) displays a list of how much of each type of power source you would need to power this grid if running everything at maximum
 *  
 *  [ COCKPIT LCDs ]
 *  Cockpits are a bit more complicated, type options into the "Argument" field in the programming block like this:
 *  
 *  cockpit <NAME> # -<TYPE>
 *  
 *  <NAME> is the name of the desired cockpit to change
 *  # is the screen number to change
 *  <TYPE> (*with* the hyphen '-' in front of it!) is one of the panel options listed above (percent, ore, etc.).
 *  
 *  [ OTHER OPTIONS ]
 *  You can also run the script with these arguments:
 *  [NEW] "findall"                            - updates all types of blocks that the script keeps track of (panels, containers, and power producers)
 *  [NEW] "findpanels"                         - looks for newly-placed screens and updates them
 *  [NEW] "findcontainers"                     - looks for newly-placed blocks with inventories to count
 *  [NEW] "findpower"                          - looks for newly-placed power-producing blocks
 *  "update_off"                               - stops the script from updating all screens
 *  "update_on"                                - continue updating screens after using "update_off"
 *  [NEW] "backgroundcolor <NAME> <COLOR>"     - to set the background color of all panels containing <NAME> in their name (does not work for cockpits)
 *  [NEW] "fontcolor <NAME> <COLOR>"           - to set the font color of all panels containing <NAME> in their name (does not work for cockpits)
 *  [NEW] "power_assessment"                   - to display information to powerreq and powerneeds panels (this does not get updated automatically, it is a one-time run)
 * 
 * 
 *  [end readme]
 */
MyCommandLine ğ=new MyCommandLine();Dictionary<string,Action>Ġ=new Dictionary<string,Action>(StringComparer.
OrdinalIgnoreCase);const UpdateType ġ=UpdateType.Trigger|UpdateType.Terminal;IMyTextSurface Ģ;IMyTextSurface ģ;IMyCubeGrid Ĥ;IMyCockpit ĥ
;IMyTextSurface Ħ;Dictionary<IMyTextSurface,string>ħ=new Dictionary<IMyTextSurface,string>();Dictionary<IMyTextPanel,
string>Ĩ=new Dictionary<IMyTextPanel,string>();List<IMyCockpit>ĩ=new List<IMyCockpit>();List<IMyTerminalBlock>Ī=new List<
IMyTerminalBlock>();List<MyInventoryItem>ī=new List<MyInventoryItem>();List<IMyPowerProducer>ĭ=new List<IMyPowerProducer>();List<
IMyBatteryBlock>Ļ=new List<IMyBatteryBlock>();Dictionary<IMyTextPanel,Color>Į=new Dictionary<IMyTextPanel,Color>();Dictionary<
IMyTextPanel,Color>į=new Dictionary<IMyTextPanel,Color>();Dictionary<IMyTextSurface,Color>İ=new Dictionary<IMyTextSurface,Color>();
Dictionary<IMyTextSurface,Color>ı=new Dictionary<IMyTextSurface,Color>();double Ĳ=120;double ĳ=120;double Ĵ=120;TimeSpan ĵ=new
TimeSpan(0,0,-60);TimeSpan Ķ=new TimeSpan(0,0,-30);TimeSpan ķ=new TimeSpan(0,0,0);TimeSpan ĸ=new TimeSpan(0,0,5);int Ĺ=0;int ĺ=0
;int ļ=0;int Ĭ=0;int Ğ=0;int Đ=0;Dictionary<string,double>ă=new Dictionary<string,double>();Dictionary<string,string>Ą=
new Dictionary<string,string>();Dictionary<string,double>ą=new Dictionary<string,double>();Dictionary<string,string>Ć=new
Dictionary<string,string>();double ć=0;double Ĉ=0;double ĉ=0;double Ċ=0;double ċ;string Č;string č;string Ď;string ď;string đ;
string ĝ;string Ē;string ē;string Ĕ;string ĕ;string Ė="";int ė=0;int Ę=15;Program(){Runtime.UpdateFrequency=UpdateFrequency.
None;Ġ["findall"]=Ě;Ġ["findpanels"]=ě;Ġ["findcontainers"]=Ľ;Ġ["findpower"]=Ŗ;Ġ["cockpit"]=Ř;Ġ["update_on"]=Ň;Ġ["update_off"]
=ŉ;Ġ["power_assessment"]=J;Ġ["backgroundcolor"]=Ū;Ġ["fontcolor"]=ū;Ģ=Me.GetSurface(0);ģ=Me.GetSurface(1);Ĥ=Me.CubeGrid;Ģ.
ContentType=ContentType.TEXT_AND_IMAGE;Ģ.FontSize=1.0f;Ģ.Alignment=TextAlignment.CENTER;Ģ.TextPadding=39.3f;Ģ.FontColor=Color.
DarkGreen;ģ.ContentType=ContentType.TEXT_AND_IMAGE;ģ.FontSize=2.6f;ģ.Alignment=TextAlignment.CENTER;ģ.TextPadding=14.5f;ģ.
FontColor=Color.DarkGreen;Ą.Add("Cobalt"," Co");Ą.Add("Gold"," Au");Ą.Add("Ice","Ice");Ą.Add("Iron"," Fe");Ą.Add("Magnesium",
" Mg");Ą.Add("Nickel"," Ni");Ą.Add("Platinum"," Pt");Ą.Add("Scrap","Scr");Ą.Add("Silicon"," Si");Ą.Add("Silver"," Ag");Ą.Add(
"Stone","Stn");Ą.Add("Uranium","  U");Ć.Add("Cobalt","Co");Ć.Add("Gold","Au");Ć.Add("Iron","Fe");Ć.Add("Magnesium","Mg");Ć.Add(
"Nickel","Ni");Ć.Add("Platinum","Pt");Ć.Add("Silicon","Si");Ć.Add("Silver","Ag");Ć.Add("Uranium"," U");foreach(KeyValuePair<
string,string>ę in Ą){ă.Add(ę.Key,0);}foreach(KeyValuePair<string,string>ę in Ć){ą.Add(ę.Key,0);}Ě();Ň();Echo(
$"\n|-{Ĥ.CustomName}-|");Echo($"{Ī.Count} inventoried blocks found.");Echo($"{Ĩ.Count} usable panels found.");Echo(
$"{ĭ.Count} power producers found.");}void Ě(){ě();Ľ();Ŗ();}void ě(){Ĺ=0;ļ=0;Ğ=0;List<IMyTextPanel>Ĝ=new List<IMyTextPanel>();GridTerminalSystem.
GetBlocksOfType<IMyTextPanel>(Ĝ,Ă=>Ă.CustomName.Contains("invPanel"));if(Ĝ==null){return;}foreach(var N in Ĝ){if(N.IsSameConstructAs(Me
)){N.Alignment=TextAlignment.LEFT;N.Font="Monospace";N.ContentType=ContentType.TEXT_AND_IMAGE;N.Enabled=true;N.
TextPadding=1.5f;N.FontSize=0.5f;if(N.CustomName.Contains("percent")){Ĺ++;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"percent");}else{Ĩ[N]=
"percent";}N.Alignment=TextAlignment.CENTER;N.FontSize=2.5f;N.TextPadding=16.0f;}else if(N.CustomName.Contains("storedpower")){ļ
++;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"storedpower");}else{Ĩ[N]="storedpower";}N.Alignment=TextAlignment.CENTER;N.FontSize=2.0f
;N.TextPadding=21.0f;}else if(N.CustomName.Contains("powerreq")){ļ++;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"powerreq");}else{Ĩ[N]
="powerreq";}N.Alignment=TextAlignment.LEFT;N.FontSize=0.4f;N.TextPadding=2.0f;}else if(N.CustomName.Contains(
"powerneeds")){ļ++;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"powerneeds");}else{Ĩ[N]="powerneeds";}N.Alignment=TextAlignment.LEFT;N.FontSize=
0.4f;N.TextPadding=2.0f;}else if(N.CustomName.Contains("power")){ļ++;N.FontSize=0.42f;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"power")
;}else{Ĩ[N]="power";}}else if(N.CustomName.Contains("batteries")){ļ++;Ğ++;N.FontSize=1.5f;N.TextPadding=34f;N.Alignment=
TextAlignment.CENTER;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"batteries");}else{Ĩ[N]="batteries";}if(!Į.ContainsKey(N)){Į.Add(N,N.
BackgroundColor);}else{Į[N]=N.BackgroundColor;}if(!į.ContainsKey(N)){į.Add(N,N.FontColor);}else{į[N]=N.FontColor;}}else if(N.CustomName
.Contains("ingotslist")){Ĺ++;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"ingotslist");}else{Ĩ[N]="ingotslist";}}else if(N.CustomName.
Contains("ingots")){Ĺ++;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"ingots");}else{Ĩ[N]="ingots";}}else if(N.CustomName.Contains("orelist")){
Ĺ++;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"orelist");}else{Ĩ[N]="orelist";}}else if(N.CustomName.Contains("ore")){Ĺ++;N.FontSize=
0.5f;if(!Ĩ.ContainsKey(N)){Ĩ.Add(N,"ore");}else{Ĩ[N]="ore";}}else if(N.CustomName.Contains("mass")){Ĺ++;if(!Ĩ.ContainsKey(N)
){Ĩ.Add(N,"mass");}else{Ĩ[N]="mass";}N.Alignment=TextAlignment.CENTER;N.FontSize=1.75f;N.TextPadding=36.0f;}}}}void Ľ(){
GridTerminalSystem.GetBlocks(Ī);var Q=Ī.ToArray();foreach(IMyTerminalBlock R in Q){if(R.HasInventory==false|R.IsSameConstructAs(Me)==false
){Ī.Remove(R);}}}void Ŗ(){GridTerminalSystem.GetBlocksOfType<IMyPowerProducer>(ĭ,ŗ=>ŗ.IsSameConstructAs(Me));}void Ř(){ĺ=
0;Ĭ=0;Đ=0;GridTerminalSystem.GetBlocksOfType<IMyCockpit>(ĩ,ř=>ř.IsSameConstructAs(Me));if(ĩ==null){Echo(
"Could not find any cockpits.");return;}string Ś=ğ.Argument(1);foreach(var ř in ĩ){if(ř.CustomName.Equals(ğ.Argument(1),StringComparison.
OrdinalIgnoreCase)){ĥ=ř;}}if(ĥ==null){Echo($"Could not find a cockpit with the name '{Ś}'.");return;}if(ğ.Argument(2).ToString()==null){
Echo("No screen index given. Use 'cockpit <COCKPITNAME> <SCREENNUMBER>' to set which LCD on the cockpit you want to display this."
);return;}int ś=int.Parse(ğ.Argument(2));Echo($"Found cockpit '{ĥ.CustomName}'.");Ħ=ĥ.GetSurface(ś);if(Ħ==null){Echo(
$"Could not find a screen with index {ś} on {ĥ.CustomName}.");return;}bool Ŝ=ğ.Switch("noformat");bool ŝ=ğ.Switch("alignleft");bool Ş=ğ.Switch("alignright");bool ş=ğ.Switch(
"aligncenter");if(!Ŝ){if(ŝ){Ħ.Alignment=TextAlignment.LEFT;}else if(Ş){Ħ.Alignment=TextAlignment.RIGHT;}else if(ş){Ħ.Alignment=
TextAlignment.CENTER;}Ħ.ContentType=ContentType.TEXT_AND_IMAGE;Ħ.TextPadding=2.5f;Ħ.FontSize=0.7f;}string Š="";bool š=ğ.Switch(
"percent");bool Ţ=ğ.Switch("orelist");bool ţ=ğ.Switch("ingotslist");bool Ť=ğ.Switch("ore");bool ť=ğ.Switch("ingots");bool Ŧ=ğ.
Switch("mass");bool ŧ=ğ.Switch("power");bool Ũ=ğ.Switch("storedpower");bool ũ=ğ.Switch("batteries");if(š){Š="percent";ĺ++;if(!
Ŝ){Ħ.Alignment=TextAlignment.CENTER;Ħ.FontSize=3.0f;Ħ.TextPadding=18f;}}else if(Ţ){Š="orelist";ĺ++;if(!Ŝ){Ħ.FontSize=
0.85f;Ħ.TextPadding=2.0f;}}else if(ţ){Š="ingotslist";ĺ++;if(!Ŝ){Ħ.FontSize=0.85f;Ħ.TextPadding=2.0f;}}else if(Ŧ){Š="mass";ĺ++
;if(!Ŝ){Ħ.FontSize=2.0f;Ħ.TextPadding=30f;}}else if(Ť){Š="ore";ĺ++;if(!Ŝ){Ħ.FontSize=0.6f;}}else if(ť){Š="ingots";ĺ++;if(
!Ŝ){Ħ.FontSize=0.6f;}}else if(ŧ){Š="power";Ĭ++;if(!Ŝ){Ħ.FontSize=0.6f;}}else if(Ũ){Š="storedpower";Ĭ++;if(!Ŝ){Ħ.FontSize=
1.2f;Ħ.TextPadding=18f;}}else if(ũ){Ĭ++;Đ++;if(!Ŝ){Ħ.FontSize=1.0f;Ħ.TextPadding=34f;}Š="batteries";if(!İ.ContainsKey(Ħ)){İ.
Add(Ħ,Ħ.BackgroundColor);}else{İ[Ħ]=Ħ.BackgroundColor;}if(!ı.ContainsKey(Ħ)){ı.Add(Ħ,Ħ.FontColor);}else{ı[Ħ]=Ħ.FontColor;}}
if(ħ.ContainsKey(Ħ)){ħ[Ħ]=Š;}else{ħ.Add(Ħ,Š);}}void Ū(){Color ľ;List<IMyTextPanel>ň;ŕ(out ľ,out ň);foreach(IMyTextPanel N
in ň){if(Į.ContainsKey(N)){Į[N]=ľ;}N.BackgroundColor=ľ;}Echo(
"Successfully updated background colors for the selected panels.");}void ū(){Color ľ;List<IMyTextPanel>ň;ŕ(out ľ,out ň,false);foreach(IMyTextPanel N in ň){if(į.ContainsKey(N)){į[N]=ľ;}N
.FontColor=ľ;}Echo("Successfully updated font colors for the selected panels.");}void ŕ(out Color ľ,out List<IMyTextPanel
>ň,bool Ŀ=true){ľ=Color.Black;ň=null;if(ğ.Argument(1)==null){Echo(
"Error: You need to specify a name for the panel(s) you want to change.");return;}else if(ğ.Argument(2)==null){Echo("Error: You need to specify the color you want to set.");return;}string ŀ=ğ.
Argument(1).ToString();if(!Ł(ğ.Argument(2),out ľ,Ŀ)){Echo("Error: there was a problem the color you entered. Color should formatted as: r:g:b or r:g:b:a where r,g,b (and the optional a) are integers 0-255."
);return;}ň=new List<IMyTextPanel>();GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(ň,N=>N.IsSameConstructAs(Me)&N.
CustomName.Contains(ŀ));if(ň==null|ň.Count==0){Echo("Error: could not find any panels with that name.");return;}}bool Ł(string ł,
out Color ľ,bool Ŀ=true){string[]Ń=ł.Split(':');ľ=new Color(0);int ń;int Ņ;int S;int ņ=1;if(!int.TryParse(Ń[0],out ń)|!int.
TryParse(Ń[1],out Ņ)|!int.TryParse(Ń[2],out S)){return false;}if(Ń.Length>=4){if(!int.TryParse(Ń[3],out ņ)){return false;}}if(Ŀ)
{ľ=new Color(ń,Ņ,S,ņ);}else{ľ=new Color(ń,Ņ,S);}return true;}void Ň(){if((Ğ|Đ)>0&ĸ.TotalMinutes<5){Runtime.
UpdateFrequency=UpdateFrequency.Update10|UpdateFrequency.Update100;}else{Runtime.UpdateFrequency=UpdateFrequency.Update100;}Ģ.WriteText
("VB86_Inventory_LCDs\nv1.4\n\n- Online -");}void ŉ(){Runtime.UpdateFrequency=UpdateFrequency.None;Ģ.FontColor=Color.Red;
Ģ.WriteText("VB86_Inventory_LCDs\nv1.4\n\n- OFFLINE -");}void œ(){ć=0;Ĉ=0;ĉ=0;Ċ=0;foreach(KeyValuePair<string,string>ę in
Ą){ă[ę.Key]=0;}foreach(KeyValuePair<string,string>ę in Ć){ą[ę.Key]=0;}string Ŋ;for(int s=0;s<Ī.Count;s++){for(int ë=0;ë<Ī
[s].InventoryCount;ë++){ī.Clear();IMyInventory ŋ=Ī[s].GetInventory(ë);ŋ.GetItems(ī);foreach(MyInventoryItem Ō in ī){
string ō=Ō.Type.SubtypeId.ToString();if(Ō.Type.TypeId=="MyObjectBuilder_Ore"&ă.ContainsKey(ō)){ă[ō]+=(double)Ō.Amount;}else if
(Ō.Type.TypeId=="MyObjectBuilder_Ingot"&ą.ContainsKey(ō)){ą[ō]+=(double)Ō.Amount;double Ŏ=(double)Ō.Amount;}}Ċ+=(double)ŋ
.CurrentMass;Ĉ+=(double)ŋ.CurrentVolume;ĉ+=(double)ŋ.MaxVolume;}}ÿ(Ĉ,ĉ,out ć,out Ŋ);Ĉ*=1000;ĉ*=1000;string ŏ="L";if(Ĉ>=
1000&Ĉ<1000000){Ĉ/=1000;ĉ/=1000;ŏ="kL";}else if(Ĉ>=1000000){Ĉ/=1000000;ĉ/=1000000;ŏ="ML";}double Ő=(double)Ċ;string ő="kg";
if(Ő>=1000&Ő<1000000){Ő/=1000;ő="t";}else if(Ő>=1000000){Ő/=1000000;ő="kt";}đ=
$"{Ĉ.ToString("N2")} / {ĉ.ToString("N2")}{ŏ}";ĝ=$"{Ő.ToString("N2")}{ő}\nGrid Mass";string Œ=$" {ć.ToString("N2")}%\nVolume: {đ} \n  Mass: {ĝ}\n";string Ŕ=Ŋ+Œ;Ď=
$"{ć.ToString("N2")}%\nINVENTORY\nUSED";ď=$"{ć.ToString("N2")}%\nINVENTORY\nUSED";Ē="\n";foreach(var Ŭ in ă){Ē+=$"{Ą[Ŭ.Key]}: {Ŭ.Value.ToString("N2")}kg\n";}ē=
"\n";foreach(var ā in ą){ē+=$"{Ć[ā.Key]}: {ā.Value.ToString("N2")}kg\n";}Č="| - - - INVENTORY - - - |\n"+Ŋ+Œ+
"\n| - - - ORE - - - |\n"+Ē;č="| - - - INVENTORY - - - |\n"+Ŋ+Œ+"\n| - - - INGOTS - - - |\n"+ē;}void ÿ(double m,double n,out double G,out string
F){F="[";G=(m/n)*100;if(G>=99.9){G=100;}int o=(int)Math.Floor(G/4);int q=25-o;for(int s=0;s<o;s++){F+="|";}for(int s=0;s<
q;s++){F+=" ";}F+="]";}string t(string u,float m,float n){string v="";double G=0;ÿ(m,n,out G,out v);string Ç=v;Ç+=
$" {G.ToString("N2")}% {u} ({m.ToString("N2")}MW)\n";return Ç;}void x(){float y=0;float z=0;float ª=0;float µ=0;float º=0;float À=0;float Á=0;float Â=0;float Ã=0;float Ä=0;
float Å=0;float Æ=0;float È=0;foreach(IMyPowerProducer w in ĭ){y+=w.MaxOutput;z+=w.CurrentOutput;string k=w.BlockDefinition.
ToString();if(w is IMyBatteryBlock){IMyBatteryBlock O=(IMyBatteryBlock)w;µ+=O.CurrentStoredPower;º+=O.MaxStoredPower;À+=O.
CurrentInput;Â+=O.CurrentOutput;Á+=O.MaxInput;Ã+=O.MaxOutput;ª++;}else if(k.Contains("WindTurbine")){È+=w.CurrentOutput;}else if(w
is IMySolarPanel){IMySolarPanel B=(IMySolarPanel)w;Ä+=B.CurrentOutput;}else if(w is IMyReactor){IMyReactor C=(IMyReactor)w
;Å+=C.CurrentOutput;}else if(k.Contains("HydrogenEngine")){Æ+=w.CurrentOutput;}}float D=µ/(Â-À);if(D!=0){ĸ=TimeSpan.
FromHours(D);string E=$"{ĸ.Days.ToString("00")}d:{ĸ.Hours.ToString("00")}h:{ĸ.Minutes.ToString("00")}m:{Math.Round((double)ĸ.Seconds,0).ToString("00")}s"
;Ė=$"Batteries\nDeplete In:\n{E}";}else{Ė="BATTERIES DEPLETED";ĸ=TimeSpan.FromSeconds(0);}string F;double G;ÿ(z,y,out G,
out F);Ĕ="| - - - TOTAL POWER OUTPUT INFO - - - -|\n\n";Ĕ+=F;Ĕ+=$" {G.ToString("N2")}% USAGE\n";Ĕ+=
$"{z.ToString("N2")} / {y.ToString("N2")} MW";Ĕ+="\n\n";string H;double I;ÿ(µ,º,out I,out H);ĕ=$"{I.ToString("N2")}%\nStored Power\nRemaining";Ĕ+=
"| - - - BATTERY INFO - - - - - - - - |\n\n";Ĕ+=H;Ĕ+=$" {I.ToString("N2")}% CAPACITY\n";Ĕ+=$"{µ.ToString("N2")} / {º.ToString("N2")} MW\n\n";Ĕ+=
$"{ª} Batteries On Grid\n";Ĕ+=$"Current/Max Total Input: {À.ToString("N2")} / {Á.ToString("N2")} MW\n";Ĕ+=
$"Current/Max Total Output: {Â.ToString("N2")} / {Ã.ToString("N2")} MW\n\n";Ĕ+="| - - - OUTPUT DISTRIBUTION INFO - - - |\n\n";Ĕ+=t("BATTERIES",Â,z);Ĕ+=t("WIND",È,z);Ĕ+=t("SOLAR",Ä,z);Ĕ+=t(
"REACTOR",Å,z);Ĕ+=t("HYDROGEN",Æ,z);}void J(){bool K=false;List<IMyTextPanel>L=new List<IMyTextPanel>();List<IMyTextPanel>M=new
List<IMyTextPanel>();foreach(var N in Ĩ){if(N.Value=="powerreq"){K=true;L.Add(N.Key);}else if(N.Value=="powerneeds"){K=true;
M.Add(N.Key);}}if(K){double P=0;Echo("Power assessment starting...\n");List<IMyTerminalBlock>f=new List<IMyTerminalBlock>
();GridTerminalSystem.GetBlocks(f);Array Q=f.ToArray();foreach(IMyTerminalBlock R in Q){if(R is IMyPowerProducer){
IMyPowerProducer S=(IMyPowerProducer)R;P+=S.MaxOutput;}}foreach(IMyTerminalBlock R in Q){if(!R.DetailedInfo.Contains("Input")){f.Remove(
R);}if(R is IMyBatteryBlock){f.Remove(R);}}float T=0;string U="";string V="";string W=
$"This {Me.CubeGrid.GridSizeEnum.ToString()} Grid Requires:\n\n";float X=12f;float Y=4.32f;float Z=0.2f;float d=0.12f;float e=0.03f;float A=0.4f;float h=0.5f;float É=15f;float â=14.75f
;float ã=300f;float ä=0.5f;float å=5f;foreach(IMyTerminalBlock R in f){var æ=R.DetailedInfo.Split('\n');for(var s=0;s<æ.
Length;s++){if(æ[s].Contains("Max Required Input")){var ç=æ[s].ToString().Split(':');var è=ç[1].ToString().Split(new char[]{
' '},StringSplitOptions.RemoveEmptyEntries);if(!float.TryParse(è[0],out T)){Echo(
$"Could not parse {è[0]} (splitMRILine[0] as a float for {R.CustomName}");continue;}U=è[1];int é=30;int ê=R.CustomName.Length<é?R.CustomName.Length:é;V+=$"-{R.CustomName.Substring(0,ê)}";if(R.
CustomName.Length>é){V+="...";}else{for(var ë=0;ë<((é+3)-R.CustomName.Length);ë++){V+=" ";}}V+=$"            [ {T} {U} ] \n";
switch(U){case"kW":T/=1000;break;case"MW":break;case"W":T/=1000000;break;}ċ+=T;}}}U="MW";float ì=(float)ċ;float í=(float)ċ-(
float)P;double î=Math.Ceiling((ì/X));double ï=Math.Ceiling(ì/Y);double ð=Math.Ceiling(ì/Z);double ñ=Math.Ceiling(ì/A);double
ò=Math.Ceiling(ì/d);double ó=Math.Ceiling(ì/e);double ô=Math.Ceiling(ì/h);double õ=Math.Ceiling(ì/É);double ö=Math.
Ceiling(ì/â);double ø=Math.Ceiling(ì/ã);double ù=Math.Ceiling(ì/ä);double ú=Math.Ceiling(ì/å);double û=Math.Ceiling((í/X));
double ü=Math.Ceiling(í/Y);double ý=Math.Ceiling(í/Z);double þ=Math.Ceiling(í/A);double Ā=Math.Ceiling(í/d);double á=Math.
Ceiling(í/e);double Ê=Math.Ceiling(í/h);double Ø=Math.Ceiling(í/É);double Ë=Math.Ceiling(í/â);double Ì=Math.Ceiling(í/ã);double
Í=Math.Ceiling(í/ä);double Î=Math.Ceiling(í/å);Dictionary<string,double[]>Ï=new Dictionary<string,double[]>();Ï.Add(
"Large Grid Batteries",new double[]{î,û});Ï.Add("Small Grid Batteries",new double[]{ï,ü});Ï.Add("Small Grid Small Batteries",new double[]{ð,ý}
);Ï.Add("Large Grid Wind Turbines (At \"Optimal\")",new double[]{ñ,þ});Ï.Add(
"Large Grid Solar Panels (At Peak Solar Input)",new double[]{ò,Ā});Ï.Add("Small Grid Solar Panels (At Peak Solar Input)",new double[]{ó,á});Ï.Add(
"Small Grid Hydrogen Engines",new double[]{ù,Í});Ï.Add("Large Grid Hydrogen Engines",new double[]{ú,Î});Ï.Add("Small Grid Small Reactors",new double[
]{ô,Ê});Ï.Add("Small Grid Large Reactors",new double[]{ö,Ë});Ï.Add("Large Grid Small Reactors",new double[]{õ,Ø});Ï.Add(
"Large Grid Large Reactors",new double[]{ø,Ì});string Ð=Me.CubeGrid.GridSizeEnum.ToString();foreach(KeyValuePair<string,double[]>Ñ in Ï){if(Ñ.Key.
Contains($"{Ð} Grid")){W+=$"{Ñ.Key.Substring(11)}:\n";W+=$"  :{Ñ.Value[0]} Total\n";if(Ñ.Value[1]>0){W+=
$"  :You Need At Least {Ñ.Value[1]} More\n\n";}else{W+=$"  :No More Should Be Needed\n\n";}}}ċ=Math.Round(ċ,2);if(ċ<1){ċ*=1000;U="kW";}if(ċ<1){ċ*=1000;U="W";}V=$"Total power requirement for this grid is approximately:\n{ċ} {U}.\n\nPress 'F' to read this panel and scroll info.\n\n"
+V;foreach(IMyTextPanel Ò in L){Ò.WriteText(V);}foreach(IMyTextPanel Ó in M){Ó.WriteText(W);}Echo(
"Power assessment complete. Check panels!");}else{Echo("Could not find a panel to output to. Create a panel named \"invPanel powerreq\" and/or a panel named \"invPanel powerneeds\"."
);}}void Ô(KeyValuePair<IMyTextPanel,string>N){Color Õ=Į[N.Key];Color Ö=į[N.Key];N.Key.WriteText(Ė);if(ĸ.TotalMinutes<5){
float Ù=(((float)ė/255)*2)-1;if(Ù<0){N.Key.BackgroundColor=Vector3.Lerp(Color.Black.ColorToHSV(),Color.Red.ColorToHSV(),1-
Math.Abs(Ù)).HSVtoColor();}else{N.Key.BackgroundColor=Vector3.Lerp(Color.Red.ColorToHSV(),Color.Black.ColorToHSV(),Ù).
HSVtoColor();}N.Key.FontColor=Ù>=-0.25&Ù<=0.25?Color.White:Color.Red;if(ė>=255){ė=0;}else{ė+=Ę;}}else{N.Key.FontColor=Ö;N.Key.
BackgroundColor=Õ;}}void Ô(KeyValuePair<IMyTextSurface,string>N){Color Õ=İ[N.Key];Color Ö=ı[N.Key];N.Key.WriteText(Ė);if(ĸ.TotalMinutes
<5){float Ù=(((float)ė/255)*2)-1;if(Ù<0){N.Key.BackgroundColor=Vector3.Lerp(Õ.ColorToHSV(),Color.Red.ColorToHSV(),1-Math.
Abs(Ù)).HSVtoColor();}else{N.Key.BackgroundColor=Vector3.Lerp(Color.Red.ColorToHSV(),Õ.ColorToHSV(),Ù).HSVtoColor();}Echo(
$"{N.Key.BackgroundColor.ToString()}\nFlux = {ė}\nlerpPoint = {Ù}");N.Key.FontColor=Ù>=-0.25&Ù<=0.25?Color.White:Color.Red;if(ė>=255){ė=0;}else{ė+=Ę;}}else{N.Key.FontColor=Ö;N.Key.
BackgroundColor=Õ;}}void Main(string Ú,UpdateType Û){if((Û&ġ)!=0){if(ğ.TryParse(Ú)){Action Ü;string Ý=ğ.Argument(0);if(Ý==null){Echo(
"No command specified.");}else if(Ġ.TryGetValue(ğ.Argument(0),out Ü)){Ü();}else{Echo($"Unknown command '{Ý}'");}}}if(0!=(Û&UpdateType.Update10)
){if(Ĩ!=null&Ĩ.Count>0){foreach(var N in Ĩ){if(N.Value=="batteries"){Ô(N);}}}if(ħ!=null&ħ.Count>0){foreach(var Þ in ħ){if
(Þ.Value=="batteries"){Ô(Þ);}}}}if(0!=(Û&UpdateType.Update100)){if(ĸ.TotalMinutes<5){Runtime.UpdateFrequency=
UpdateFrequency.Update10|UpdateFrequency.Update100;}else{Runtime.UpdateFrequency=UpdateFrequency.Update100;}int ß=Ĩ.Count+ħ.Count;ģ.
WriteText($"Updating {ß} displays.\n{Ĩ.Count} panels registered.\n{ħ.Count} cockpit LCDs registered.\n{Ī.Count} inventories tracked."
);var à=TimeSpan.FromSeconds(Runtime.TimeSinceLastRun.TotalSeconds);ĵ+=à;ķ+=à;Ķ+=à;if(ĵ>TimeSpan.FromSeconds(Ĵ)){Ľ();ĵ=
TimeSpan.FromSeconds(0);}else if(ķ>TimeSpan.FromSeconds(Ĳ)){ě();ķ=TimeSpan.FromSeconds(0);}else if(Ķ>TimeSpan.FromSeconds(ĳ)){Ŗ(
);Ķ=TimeSpan.FromSeconds(0);}if((Ĺ|ĺ)>0){œ();}if((ļ|Ĭ)>0){x();}if(Ĩ!=null&Ĩ.Count>0){foreach(var N in Ĩ){switch(N.Value){
case"percent":N.Key.WriteText(Ď);break;case"ore":N.Key.WriteText(Č);break;case"orelist":N.Key.WriteText(Ē);break;case
"ingots":N.Key.WriteText(č);break;case"ingotslist":N.Key.WriteText(ē);break;case"mass":N.Key.WriteText(ĝ);break;case"power":N.
Key.WriteText(Ĕ);break;case"storedpower":N.Key.WriteText(ĕ);break;case"batteries":Ô(N);break;}}}if(ħ!=null&ħ.Count>0){
foreach(var Þ in ħ){switch(Þ.Value){case"percent":Þ.Key.WriteText(ď);break;case"orelist":Þ.Key.WriteText(Ē);break;case
"ingotslist":Þ.Key.WriteText(ē);break;case"mass":Þ.Key.WriteText(ĝ);break;case"ore":Þ.Key.WriteText(Č);break;case"ingots":Þ.Key.
WriteText(č);break;case"power":Þ.Key.WriteText(Ĕ);break;case"storedpower":Þ.Key.WriteText(ĕ);break;case"batteries":Ô(Þ);break;}}}
}}