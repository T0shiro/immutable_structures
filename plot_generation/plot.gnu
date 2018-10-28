set grid lw 3
set logscale xy 2
set xlabel "Number of elements"
set ylabel "Execution time (ms)"
set terminal svg
plot file using 1:2 '%lf (%lf,%lf)' title 'mutable' with lines smooth unique, \
file using 1:3 '%lf (%lf,%lf)' title 'immutable' with lines smooth unique