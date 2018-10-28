#!/bin/bash

for struct in avl heap
do
    gnuplot -p -e "file='plot_generation/${struct}_creation.txt'" plot_generation/plot.gnu > plot_generation/${struct}_creation.svg
    gnuplot -p -e "file='plot_generation/${struct}_deletion.txt'" plot_generation/plot.gnu > plot_generation/${struct}_deletion.svg
    gnuplot -p -e "file='plot_generation/${struct}_insertion.txt'" plot_generation/plot.gnu > plot_generation/${struct}_insertion.svg
done