import pandas as pd
import click
import requests
import os
import time



@click.command()
# use the EXPORT url!
@click.option("--url", default="https://docs.google.com/spreadsheets/d/1XtTgc3RPxlhGGlqQ6u4RKui0STqpGo_H779t3h72HOA/export?format=xlsx&id=1XtTgc3RPxlhGGlqQ6u4RKui0STqpGo_H779t3h72HOA", help="the url of config table")
@click.option("--local_filename", default="config.xlsx")
@click.option("--csv_dir", default="Resources/Configs")
def download_config(url, local_filename, csv_dir):
    with requests.get(url, stream=True) as r:
        r.raise_for_status()
        with open(local_filename, 'wb') as f:
            for chunk in r.iter_content(chunk_size=8192): 
                f.write(
                    chunk)
        convert_file(local_filename, csv_dir)
    os.remove(local_filename)

def convert_file(local_filename, csv_dir):
    if not os.path.exists(csv_dir):
        os.mkdir(csv_dir)
    file = pd.read_excel(local_filename, sheet_name=None)
    for name, sheet in file.items():
        dir = csv_dir + "/" + name + ".csv"
        sheet.to_csv(dir, encoding="utf-8", index=False)

if __name__ == "__main__":
    download_config()