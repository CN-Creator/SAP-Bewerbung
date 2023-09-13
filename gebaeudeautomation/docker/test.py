import csv
import re

input_file = '/Users/niklas/Documents/testsensor2_data.csv'
output_file = '/Users/niklas/Documents/testsensor2_data_new.csv'

# Regex pattern to match and extract timestamp and value
pattern = r'(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}(?:\.\d{1,3})?\+\d{2}),(?:"?([\d\.]+)"?)'

with open(input_file, 'r') as infile, open(output_file, 'w', newline='') as outfile:
    reader = csv.reader(infile)
    writer = csv.writer(outfile)

    for row in reader:
        corrected_row = []
        for cell in row:
            match = re.search(pattern, cell)
            if match:
                timestamp, value = match.groups()
                corrected_row.extend([timestamp, value])
            else:
                corrected_row.append(cell)
        writer.writerow(corrected_row)