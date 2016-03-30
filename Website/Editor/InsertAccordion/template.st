<aside class="accordion">
    <h4>
        $Headline$
    </h4>
    <ul>
		$Sectors:{Sector|
        <li class="tab">
			$if(Sector.IsOpen)$
			<input id="tab-$Sector.Id$-$i$" type="checkbox" class="checked" />            
			$else$
			<input id="tab-$Sector.Id$-$i$" type="checkbox" />			
			$endif$
            <label for="tab-$Sector.Id$-$i$">
				$Sector.Title$ 
            </label>
            <div class="tab__content">
            	$Sector.Text$
            </div>
        </li>
		}$
    </ul>
</aside>