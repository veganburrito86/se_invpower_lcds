 -----------
 VB86_Inventory_LCDs v1.4
 by VeganBurrito86
 
 Script to count all inventory and display ore lists, ingot lists, volume and mass information, and various power information.

 In this update, many optimizations were made. Most notably, newly-placed blocks do not get updated as quickly as they did before. Panels, containers, and power producers are automatically searched for every 120 seconds, but each of these three calls happen in offset from each other. This was done to drastrically save on the number of calls (and the number of simultaneous calls) to search through all blocks on your grids, which can easily cause lag. If you do not see newly-placed blocks right away, you could either simply hit "Recompile" on the programming block to update everything, or, see the other options at the bottom of this readme to update all blocks or certain types of blocks.
 
 [ PANELS ]
 Name your LCD panels "invPanel XXXXXX", replace "XXXXXX" with one of the following options:
 
 percent            - simply displays the percentage of inventory used up
 ore                - displays a list of all ore in grid as well as % of inventory used at top
 ingots             - displays a list of all ingots in grid as well as % of inventory used at top
 orelist            - displays only a list of ore
 ingotslist         - displays only a list of ingots
 mass               - displays the total mass of the grid
 power              - displays total output, stored power remaining, # of batteries, total battery input & output, and % of output distribution for each type of power source
 storedpower        - simply displays the percentage of stored power remaining
 [NEW] batteries    - displays the remaining amount of time until your batteries are depleted
 [NEW] powerreq     - (must use power_assessment argument) displays total estimated power needed for entire grid and a list of all powered blocks and how much power they take running at max
 [NEW] powerneeds   - (must use power_assessment argument) displays a list of how much of each type of power source you would need to power this grid if running everything at maximum
 
 [ COCKPIT LCDs ]
 Cockpits are a bit more complicated, type options into the "Argument" field in the programming block like this:
 
 cockpit <NAME> # -<TYPE>
 
 <NAME> is the name of the desired cockpit to change
 # is the screen number to change
 <TYPE> (*with* the hyphen '-' in front of it!) is one of the panel options listed above (percent, ore, etc.).
 
 [ OTHER OPTIONS ]
 You can also run the script with these arguments:
 [NEW] "findall"                            - updates all types of blocks that the script keeps track of (panels, containers, and power producers)
 [NEW] "findpanels"                         - looks for newly-placed screens and updates them
 [NEW] "findcontainers"                     - looks for newly-placed blocks with inventories to count
 [NEW] "findpower"                          - looks for newly-placed power-producing blocks
 "update_off"                               - stops the script from updating all screens
 "update_on"                                - continue updating screens after using "update_off"
 [NEW] "backgroundcolor <NAME> <COLOR>"     - to set the background color of all panels containing <NAME> in their name (does not work for cockpits)
 [NEW] "fontcolor <NAME> <COLOR>"           - to set the font color of all panels containing <NAME> in their name (does not work for cockpits)
 [NEW] "power_assessment"                   - to display information to powerreq and powerneeds panels (this does not get updated automatically, it is a one-time run)


 [end readme]