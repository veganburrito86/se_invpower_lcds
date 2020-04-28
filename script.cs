/*
 * R e a d m e
 * -----------
 * VB86_Inventory_LCDs v1.2
 * by VeganBurrito86
 * 
 * Script to count all inventory and display ore lists, ingot lists, volume and mass information, and power information.
 * 
 * Name your LCD panels "invPanel ore", "invPanel ingots", or "invPanel power"
 * 
 * Cockpits are more complicated, use Argument as follows:
 * 
 * cockpit <NAME> # -<TYPE>
 * 
 * where <NAME> is the name of the desired cockpit to change, # is the screen number to change, and <TYPE> (*with* the hyphen '-' in front of it!) is one of the following:
 * 
 * -percent
 * -orelist
 * -ingotslist
 * -ore
 * -ingots
 * -mass
 * -power
 * -storedpower
 * 
 * - - - "-ore" and "-ingots" are the list of those items AND some additional inventory information on one screen (%inventory used, total volume and mass)
 * - - - "-power" shows the full power information screen, while "-storedpower" just shows a percent of stored power remaining.
 * 
 */
MyCommandLine Ù =new MyCommandLine();Dictionary<string,Action>Ú=new Dictionary<string,Action>(StringComparer.
OrdinalIgnoreCase);const UpdateType Û=UpdateType.Trigger|UpdateType.Terminal;IMyTextSurface Ü;IMyTextSurface Ý;IMyCubeGrid Þ;IMyCockpit à
;IMyTextSurface ç;Dictionary<IMyTextSurface,string>á=new Dictionary<IMyTextSurface,string>();Dictionary<IMyTextPanel,
string>â=new Dictionary<IMyTextPanel,string>();List<IMyTerminalBlock>ã=new List<IMyTerminalBlock>();string ä;string å;string æ
;string è;string ß;string Ø;string Î;string É;string Ê;string Ë;Program(){Runtime.UpdateFrequency=UpdateFrequency.None;Ú[
"initpanels"]=Ì;Ú["findcontainers"]=Ð;Ú["cockpit"]=Ó;Ú["update_on"]=Ą;Ú["update_off"]=ą;Ü=Me.GetSurface(0);Ý=Me.GetSurface(1);Þ=Me.
CubeGrid;Ü.ContentType=ContentType.TEXT_AND_IMAGE;Ü.FontSize=1.0f;Ü.Alignment=TextAlignment.CENTER;Ü.TextPadding=39.3f;Ü.
FontColor=Color.DarkGreen;Ý.ContentType=ContentType.TEXT_AND_IMAGE;Ý.FontSize=2.6f;Ý.Alignment=TextAlignment.CENTER;Ý.TextPadding
=14.5f;Ý.FontColor=Color.DarkGreen;Ì();Ð();Ą();Echo($"\n|-{Þ.CustomName}-|");Echo(
$"{ã.Count} inventories initially found.");}void Ì(){List<IMyTextPanel>Í=new List<IMyTextPanel>();GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(Í,Ï=>Ï.
CustomName.Contains("invPanel"));if(Í==null){return;}foreach(var Ö in Í){if(Ö.IsSameConstructAs(Me)){Echo(
$"Found panel {Ö.CustomName}...");Ö.Alignment=TextAlignment.LEFT;Ö.Font="Monospace";Ö.BackgroundColor=Color.Black;Ö.ContentType=ContentType.
TEXT_AND_IMAGE;Ö.Enabled=true;Ö.TextPadding=1.5f;Ö.FontSize=0.5f;if(Ö.CustomName.Contains("ingots")){if(!â.ContainsKey(Ö)){â.Add(Ö,
"ingots");}else{â[Ö]="ingots";}}else if(Ö.CustomName.Contains("ore")){Ö.FontSize=0.5f;if(!â.ContainsKey(Ö)){â.Add(Ö,"ore");}else
{â[Ö]="ore";}}else if(Ö.CustomName.Contains("power")){Ö.FontSize=0.42f;if(!â.ContainsKey(Ö)){â.Add(Ö,"power");}else{â[Ö]=
"power";}}}}}void Ð(){GridTerminalSystem.GetBlocks(ã);var Ñ=ã.ToArray();foreach(IMyTerminalBlock Ò in Ñ){if(Ò.HasInventory==
false|Ò.IsSameConstructAs(Me)==false){ã.Remove(Ò);}}}void Ó(){List<IMyCockpit>Ô=new List<IMyCockpit>();GridTerminalSystem.
GetBlocksOfType<IMyCockpit>(Ô,Õ=>Õ.IsSameConstructAs(Me));if(Ô==null){Echo("Could not find ANY cockpits.");return;}string È=Ù.Argument(
1);foreach(var Õ in Ô){if(Õ.CustomName.Equals(Ù.Argument(1),StringComparison.OrdinalIgnoreCase)){à=Õ;}}if(à==null){Echo(
$"Could not find a cockpit with the name '{È}'.");return;}if(Ù.Argument(2).ToString()==null){Echo("No screen index given. Use 'cockpit <COCKPITNAME> <SCREENNUMBER>' to set which LCD on the cockpit you want to display this."
);return;}int é=int.Parse(Ù.Argument(2));Echo($"Found cockpit '{à.CustomName}'.");ç=à.GetSurface(é);if(ç==null){Echo(
$"Could not find a screen with index {é} on {à.CustomName}.");return;}bool ú=Ù.Switch("noformat");if(!ú){ç.Alignment=TextAlignment.LEFT;ç.FontColor=Color.DarkGreen;ç.
BackgroundColor=Color.Black;ç.ContentType=ContentType.TEXT_AND_IMAGE;ç.TextPadding=2.5f;ç.FontSize=0.7f;}string û="";bool ü=Ù.Switch(
"percent");bool ý=Ù.Switch("orelist");bool þ=Ù.Switch("ingotslist");bool ÿ=Ù.Switch("ore");bool Ā=Ù.Switch("ingots");bool ā=Ù.
Switch("mass");bool Ă=Ù.Switch("power");bool ă=Ù.Switch("storedpower");if(ü){û="percent";if(!ú){ç.Alignment=TextAlignment.
CENTER;ç.FontSize=3.0f;ç.TextPadding=18f;}}else if(ý){û="orelist";if(!ú){ç.Alignment=TextAlignment.LEFT;ç.FontSize=0.85f;ç.
TextPadding=2.0f;}}else if(þ){û="ingotslist";if(!ú){ç.Alignment=TextAlignment.LEFT;ç.FontSize=0.85f;ç.TextPadding=2.0f;}}else if(ā)
{û="mass";if(!ú){ç.Alignment=TextAlignment.CENTER;ç.FontSize=2.0f;ç.TextPadding=30f;}}else if(ÿ){û="ore";if(!ú){ç.
FontSize=0.6f;}}else if(Ā){û="ingots";if(!ú){ç.FontSize=0.6f;}}else if(Ă){û="power";if(!ú){ç.FontSize=0.6f;}}else if(ă){û=
"storedpower";if(!ú){ç.FontSize=1.2f;ç.Alignment=TextAlignment.CENTER;ç.TextPadding=18f;}}if(á.ContainsKey(ç)){á[ç]=û;}else{á.Add(ç,û
);}}void Ą(){Runtime.UpdateFrequency=UpdateFrequency.Update100;Ü.WriteText("Inventory + Power\nLCD Program\n\n- Online -"
);}void ą(){Runtime.UpdateFrequency=UpdateFrequency.None;Ü.FontColor=Color.DarkRed;Ü.WriteText(
"Inventory + Power\nLCD Program\n\n- OFFLINE -");}void Ć(){double ć=0;MyFixedPoint Ĉ=0;MyFixedPoint ĉ=0;MyFixedPoint Ċ=0;List<MyInventoryItem>ù=new List<
MyInventoryItem>();List<MyInventoryItem>ê=new List<MyInventoryItem>();Dictionary<string,MyFixedPoint>ð=new Dictionary<string,
MyFixedPoint>();ð.Add("Cobalt",0);ð.Add("Gold",0);ð.Add("Ice",0);ð.Add("Iron",0);ð.Add("Magnesium",0);ð.Add("Nickel",0);ð.Add(
"Platinum",0);ð.Add("Scrap",0);ð.Add("Silicon",0);ð.Add("Silver",0);ð.Add("Stone",0);ð.Add("Uranium",0);Dictionary<string,string>ë
=new Dictionary<string,string>();ë.Add("Cobalt"," Co");ë.Add("Gold"," Au");ë.Add("Ice","Ice");ë.Add("Iron"," Fe");ë.Add(
"Magnesium"," Mg");ë.Add("Nickel"," Ni");ë.Add("Platinum"," Pt");ë.Add("Scrap","Scr");ë.Add("Silicon"," Si");ë.Add("Silver"," Ag");
ë.Add("Stone","Stn");ë.Add("Uranium","  U");Dictionary<string,MyFixedPoint>ì=new Dictionary<string,MyFixedPoint>();ì.Add(
"Cobalt",0);ì.Add("Gold",0);ì.Add("Iron",0);ì.Add("Magnesium",0);ì.Add("Nickel",0);ì.Add("Platinum",0);ì.Add("Silicon",0);ì.Add(
"Silver",0);ì.Add("Uranium",0);Dictionary<string,string>í=new Dictionary<string,string>();í.Add("Cobalt","Co");í.Add("Gold","Au"
);í.Add("Iron","Fe");í.Add("Magnesium","Mg");í.Add("Nickel","Ni");í.Add("Platinum","Pt");í.Add("Silicon","Si");í.Add(
"Silver","Ag");í.Add("Uranium"," U");String î;for(int S=0;S<ã.Count;S++){List<MyInventoryItem>ï=new List<MyInventoryItem>();for(
int ñ=0;ñ<ã[S].InventoryCount;ñ++){IMyInventory ø=ã[S].GetInventory(ñ);if(ø==null){continue;}ø.GetItems(ï);foreach(
MyInventoryItem ò in ï){string ó=ò.Type.SubtypeId.ToString();if(ò.Type.TypeId=="MyObjectBuilder_Ore"&ð.ContainsKey(ó)){ð[ó]+=ò.Amount;}
else if(ò.Type.TypeId=="MyObjectBuilder_Ingot"&ì.ContainsKey(ó)){ì[ó]+=ò.Amount;}}MyFixedPoint ô=ø.CurrentVolume;
MyFixedPoint õ=ø.MaxVolume;MyFixedPoint ö=ø.CurrentMass;Ċ+=ö;Ĉ+=ô;ĉ+=õ;}}ć=((double)Ĉ/(double)ĉ)*100;int n=(int)Math.Floor(ć/4);î=
"[";for(var S=n;S>0;S--){î+="|";}int Ç=25-n;if(Ç>0){for(var S=Ç;S>0;S--){î+="-";}}î+="]";double Q=(double)Ĉ*1000;double T=(
double)ĉ*1000;string U="L";if(Q>1000&Q<1000000){Q/=1000;T/=1000;U="kL";}else if(Q>1000000){Q/=1000000;T/=1000000;U="ML";}
double V=(double)Ċ;string W="kg";if(V>1000){V/=1000;W="t";}ß=$"{Q.ToString("N2")} / {T.ToString("N2")}{U}";Ø=
$"{V.ToString("N2")}{W}";string X=$"\n{ć.ToString("N2")}% Inventory Used\n\nVolume: {ß} \n  Mass: {Ø}\n";string Y=î+X;æ=
$"{ć.ToString("N2")}%\nUSED";è=$"{ć.ToString("N2")}%\nINVENTORY\nUSED";Î="\n";foreach(var a in ð){double k=(double)a.Value;Î+=
$"{ë[a.Key]}: {k.ToString("N2")}kg\n";}É="\n";foreach(var b in ì){double d=(double)b.Value;É+=$"{í[b.Key]}: {d.ToString("N2")}kg\n";}ä="|   ORE   |\n\n"+î+X+
Î;å="|   INGOTS   |\n\n"+î+X+É;}void e(){List<IMyPowerProducer>f=new List<IMyPowerProducer>();GridTerminalSystem.
GetBlocksOfType<IMyPowerProducer>(f,g=>g.IsSameConstructAs(Me));float h=0;float m=0;float Z=0;float R=0;float H=0;float B=0;float C=0;
float D=0;float E=0;float F=0;float G=0;float I=0;float P=0;foreach(IMyPowerProducer J in f){h+=J.MaxOutput;m+=J.
CurrentOutput;if(J is IMyBatteryBlock){IMyBatteryBlock K=(IMyBatteryBlock)J;R+=K.CurrentStoredPower;H+=K.MaxStoredPower;B+=K.
CurrentInput;D+=K.CurrentOutput;C+=K.MaxInput;E+=K.MaxOutput;Z++;}else if(J.BlockDefinition.TypeIdString==
"MyObjectBuilder_WindTurbine"){P+=J.CurrentOutput;}else if(J is IMySolarPanel){IMySolarPanel L=(IMySolarPanel)J;F+=L.CurrentOutput;}else if(J is
IMyReactor){IMyReactor M=(IMyReactor)J;G+=M.CurrentOutput;}else if(J.BlockDefinition.SubtypeId=="LargeHydrogenEngine"){I+=J.
CurrentOutput;}}string N="[";float O=m;float A=(m/h)*100;if(A>=99.9){A=100;}int n=(int)Math.Floor(A/4);int q=25-n;for(int S=0;S<n;S++
){N+="|";}for(int S=0;S<q;S++){N+=" ";}N+="]";string µ="MW";if(m<1){O/=1000;h/=1000;µ="kW";}Ê=
"| - - - TOTAL POWER OUTPUT INFO - - - -|\n\n";Ê+=N;Ê+=$" {A.ToString("N2")}% USAGE\n";Ê+=$"{O.ToString("N2")} / {h.ToString("N2")} {µ}";Ê+="\n\n";string º="[";float
À=(R/H)*100;if(À>=99.9){À=100;}n=(int)Math.Floor(À/4);q=25-n;for(int S=0;S<n;S++){º+="|";}for(int S=0;S<q;S++){º+=" ";}º
+="]";Ë=$"{À.ToString("N2")}%\nStored Power\nRemaining";Ê+="| - - - BATTERY INFO - - - - - - - - |\n\n";Ê+=º;Ê+=
$" {À.ToString("N2")}% CAPACITY\n";Ê+=$"{R.ToString("N2")} / {H.ToString("N2")} MW\n\n";Ê+=$"{Z} Batteries On Grid\n";Ê+=
$"Current/Max Total Input: {B.ToString("N2")} / {C.ToString("N2")} MW\n";Ê+=$"Current/Max Total Output: {D.ToString("N2")} / {E.ToString("N2")} MW\n";Ê+="\n";Ê+=
"| - - - OUTPUT DISTRIBUTION INFO - - - |\n\n";string Â="[";float Å=(D/m)*100;n=(int)Math.Floor(Å/4);q=25-n;for(int S=0;S<n;S++){Â+="|";}for(int S=0;S<q;S++){Â+=" ";}
Â+="]";Ê+=Â;Ê+=$" {Å.ToString("N2")}% BATTERIES ({D.ToString("N2")}MW)";Ê+="\n";string Ã="[";float Ä=(P/m)*100;n=(int)
Math.Floor(Ä/4);q=25-n;for(int S=0;S<n;S++){Ã+="|";}for(int S=0;S<q;S++){Ã+=" ";}Ã+="]";Ê+=Ã;Ê+=
$" {Ä.ToString("N2")}% WIND ({P.ToString("N2")}MW)";Ê+="\n";string Æ="[";float Á=(F/m)*100;n=(int)Math.Floor(Á/4);q=25-n;for(int S=0;S<n;S++){Æ+="|";}for(int S=0;S<q;S++){
Æ+=" ";}Æ+="]";Ê+=Æ;Ê+=$" {Á.ToString("N2")}% SOLAR ({F.ToString("N2")}MW)";Ê+="\n";string r="[";float s=(G/m)*100;n=(int
)Math.Floor(s/4);q=25-n;for(int S=0;S<n;S++){r+="|";}for(int S=0;S<q;S++){r+=" ";}r+="]";Ê+=r;Ê+=
$" {s.ToString("N2")}% NUCLEAR ({G.ToString("N2")}MW)";Ê+="\n";string t="[";float u=(I/m)*100;n=(int)Math.Floor(u/4);q=25-n;for(int S=0;S<n;S++){t+="|";}for(int S=0;S<q;S++){
t+=" ";}t+="]";Ê+=t;Ê+=$" {u.ToString("N2")}% HYDROGEN ({I.ToString("N2")}MW)";}void Main(string v,UpdateType w){int x=â.
Count+á.Count;Ý.WriteText($"Updating {x} displays.\n{â.Count} panels registered.\n{á.Count} cockpit LCDs registered.\n{ã.Count} inventories tracked."
);if((w&Û)!=0){if(Ù.TryParse(v)){Action y;string z=Ù.Argument(0);if(z==null){Echo("No command specified.");}else if(Ú.
TryGetValue(Ù.Argument(0),out y)){y();}else{Echo($"Unknown command '{z}'");}}}if(0!=(w&UpdateType.Update100)){Ć();e();if(â!=null){
foreach(var Ö in â){if(Ö.Value=="ore"){Ö.Key.WriteText(ä);}else if(Ö.Value=="ingots"){Ö.Key.WriteText(å);}else if(Ö.Value==
"power"){Ö.Key.WriteText(Ê);}}}if(á!=null){foreach(var ª in á){if(ª.Value=="percent"){ª.Key.WriteText(è);}else if(ª.Value==
"orelist"){ª.Key.WriteText(Î);}else if(ª.Value=="ingotslist"){ª.Key.WriteText(É);}else if(ª.Value=="mass"){ª.Key.WriteText(Ø);}
else if(ª.Value=="ore"){ª.Key.WriteText(ä);}else if(ª.Value=="ingots"){ª.Key.WriteText(å);}else if(ª.Value=="power"){ª.Key.
WriteText(Ê);}else if(ª.Value=="storedpower"){ª.Key.WriteText(Ë);}}}}}