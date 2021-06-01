import move2

userin = "setup placeholder"

move2.worlddisplay(move2.minyaxis) #initial display function, for initialization

while userin != "quit": #consistently run the following
    userin = input().lower() #take user input in lower case
    move2.axisfinder() #find the X
    move2.activeyaxis = move2.yaxislist[move2.yaxis] #set the activeyaxis to the y axis with the X
    if userin == "test": #test code, n/a
        print("test vacant")
    elif userin == "axistest": #used for testing again, n/a
        move2.axisfinder()
        print("yaxis: " + str(move2.yaxis))
        print("xaxis: " + str(move2.xaxis))
    elif userin == "a": #if user types a
        if move2.xaxis != 0: #if the X isnt on the edge
            move2.oldev = move2.ev #change old event
            move2.ev = move2.activeyaxis.pop(move2.xaxis - 1) #change new event
            move2.eventfinder(move2.ev) #run eventfinder with new event
    elif userin == "d": #if user types d
        if move2.xaxis != len(move2.activeyaxis)-1: #if the X isnt on the edge
            move2.oldev = move2.ev #change old event
            move2.ev = move2.activeyaxis.pop(move2.xaxis + 1) #change new event
            move2.eventfinder(move2.ev) #run eventfinder with new event
    elif userin == "w": #if user types w
        if move2.activeyaxis != move2.yaxis0: #if the X isnt on the edge
            try: #try to run the following
                move2.tempyaxis = move2.yaxislist[move2.yaxis - 1] #create a temporary new y axis
                move2.oldev = move2.ev #change old event
                move2.ev = move2.tempyaxis.pop(move2.xaxis) #change new event
                move2.eventfinder(move2.ev) #run eventfinder with new event
            except: #run if the try failed:
                #the only reason the try should fail is because there is nothing
                #in the direction
                print("There is nothing in that direction")
    elif userin == "s": #if user types s
        if move2.activeyaxis != move2.yaxislist[move2.minyaxis]: #if the X isnt on the edge
            try: #try to run the following
                move2.tempyaxis = move2.yaxislist[move2.yaxis + 1] #create a temporary new y axis
                move2.oldev = move2.ev #change old event
                move2.ev = move2.tempyaxis.pop(move2.xaxis) #change new event
                move2.eventfinder(move2.ev) #run eventfinder with new event
            except: #run if the try failed:
                #the only reason the try should fail is because there is nothing
                #in the direction
                print("There is nothing in that direction")
    elif userin == "quit":
        print("ending program, thanks for playing!")
    else: #run if the input is not in range of anything prior
        print("invalid input") #print invalid input to console
