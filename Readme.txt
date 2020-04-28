/*
 * R e a d m e
 * -----------
 * VB86_Inventory_LCDs v1.3
 * by VeganBurrito86
 * 
 * ***IMPORTANT NEW ARGUMENT OPTIONS AT BOTTOM OF THIS FILE*** 
 * 
 * Script to count all inventory and display ore lists, ingot lists, volume and mass information, and power information.
 * 
 * Name your LCD panels "invPanel XXXXXX", replace "XXXXXX" with one of these options:
 * 
 * percent    |    orelist    |    ingotslist    |    ore    |    ingots    |    
 * mass       |    power      |    storedpower   |
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
 * 
 * You can run the script with these arguments:
 * "initpanels" - looks for newly-placed screens and updates them
 * "findcontainers" - looks for newly-placed blocks with inventories to count
 * "update_off" - stops the script from updating screens
 * "update_on" - continue updating screens after using "update_off"
 */