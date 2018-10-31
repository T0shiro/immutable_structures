import numpy


def getCoeffFor(sortName):
    file = open(sortName + ".txt", "r")
    lines = file.readlines()
    file.close()
    x = []
    y1 = []
    y2 = []
    for line in lines:
        tab = line.split()
        valX = tab[0]
        valY1 = tab[1].replace("(", "").replace(",", "")
        valY2 = tab[2].replace(")", "")
        if int(valX) > 128:
            x.append(int(valX))
            y1.append(float(valY1))
            y2.append(float(valY2))
    covariance1 = numpy.cov(numpy.log(x), numpy.log(y1))
    covariance2 = numpy.cov(numpy.log(x), numpy.log(y2))
    return covariance1[1][0] / covariance1[0][0], covariance2[1][0] / covariance2[0][0]


results = ["avl_creation", "avl_deletion", "avl_insertion", 
    "heap_creation", "heap_deletion", "heap_insertion", 
    "heap_vs_AVL_creation", "heap_vs_AVL_deletion", "heap_vs_AVL_insertion",
    "RedBlack_vs_AVL_deletion", "RedBlack_vs_AVL_insertion", "RedBlack_vs_AVL_search", 
]
for result in results:
   res1, res2 = getCoeffFor("plot_generation/"+result)
   print(result + " : " + str(res1) + ", "+ str(res2))