#!/bin/bash

for struct in avl heap
do
    for op in creation insertion deletion
    do
        gnuplot -p -e "file='plot_generation/${struct}_$op.txt'" plot_generation/plot.gnu > plot_generation/${struct}_$op.svg
    done
done