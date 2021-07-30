import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-line-graph',
  templateUrl: './line-graph.component.html',
})
export class LineGraphComponent  {
  @Input()
  public chartData = [
    ['London', 8136000],
    ['New York', 8538000],
    ['Paris', 2244000],
    ['Berlin', 3470000],
    ['Kairo', 19500000]
  ];

  @Input()
  public chartType;
  @Input()
  public chartColumns;
  @Input()
  public chartTitle;
}
