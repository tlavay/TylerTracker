import { Component, Input } from '@angular/core';
import { GoogleChartComponent } from 'angular-google-charts';

@Component({
  selector: 'app-line-graph',
  templateUrl: './line-graph.component.html',
})
export class LineGraphComponent  {
  @Input()
  public chartData;
  @Input()
  public chartType;
  @Input()
  public chartColumns;
  @Input()
  public chartTitle;
  @Input()
  public chartOptions;
}
